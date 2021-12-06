using GameLogic;
using GameLogic.Character;
using GameLogic.Character.Components;
using GameLogic.Character.Grunts;
using GameLogic.Character.Interfaces;
using GameLogic.Character.PC;
using GameLogic.GameLogic;
using GameLogic.GameLogic.GameController;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer
{
    public class InputHandler
    {
        MongoClient client = new MongoClient("mongodb+srv://MatthewC:Vault159@cluster0.2ximt.mongodb.net/GameData?retryWrites=true&w=majority");

        public void Update(List<string> audioFiles)
        {
            //Putting in True to get the player
            RoundController rc = new RoundController();
            Biggie user = FetchBiggie("True");
            Grunt grunt = FetchGrunt();
            if (grunt == null)
            {
                //Putting in False to get the boss
                Biggie boss = FetchBiggie("False");
                rc.Update(user, boss, grunt, audioFiles);
            }
            else
            {
                rc.Update(user, null, grunt, audioFiles);
            }
        }

        public bool FightRound(string userInput, List<string> userResults, List<string> enemyResults)
        {
            UserInput move = determineUserInput(userInput);
            

            /*
             * Player Turn happens first. Add string to array.
             * Check if enemy health is zero. If zero pass back death sound.
             * Then Enemy turn. Add string to array.
             * Check if player health is zero. If zero pass back death sound.
             * Update all data to Mongo
             */
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Exists("location");
            var doc = run.Find(filter).FirstOrDefault();
            BsonValue roomLevel;
            doc.TryGetValue("level", out roomLevel);
            //Putting in True to get the player
            RoundController rc = new RoundController();
            Biggie user = FetchBiggie("True");
            bool cantDoMove = onCooldown(move, user);
            if (cantDoMove)
            {
                userResults.Add("OnCooldown");
                return false;
            }
            Grunt grunt = FetchGrunt();
            Biggie boss = null;
            if (grunt == null)
            {
                //Putting in False to get the boss
                boss = FetchBiggie("False");
                bool gameOver = FightWithBoss(move, user, boss, rc, userResults, enemyResults);
                if (boss.health < 1)
                {
                    UpdateBossDead(boss, user);
                }
                else
                {
                    UpdateCharactersBossDataMongo(user, boss);
                }

                //these are for testing
                Console.WriteLine(user.ToBson());
                Console.WriteLine(boss.ToBson());

                return FightResults(gameOver, userResults, enemyResults);

            }
            else
            {
                bool gameOver = FightWithGrunt(move, user, grunt, rc, userResults, enemyResults);
                if (grunt.health < 1)
                {
                    UpdateGruntDead(grunt, user);
                }
                else
                {
                    UpdateCharactersGruntDataMongo(user, grunt);
                }

                //these are for testing
                Console.WriteLine(user.ToBson());
                Console.WriteLine(grunt.ToBson());

                return FightResults(gameOver, userResults, enemyResults);
            }
        }

        private bool onCooldown(UserInput move, Biggie user)
        {
            /*
             * This method checks to see if the move is an ability from there is checks if the player can do the move. If it cant then it will return true;
             */
            switch (move)
            {
                case UserInput.Q:
                    //tactical
                    if (user.TacticalCooldown > 0) return true;
                    else return false;
                case UserInput.W:
                    //utility
                    if (user.UtilityCooldown > 0) return true;
                    else return false;
                case UserInput.E:
                    //ultimate
                    if (user.UltimateCooldown > 0) return true;
                    else return false;
                default:
                    return false;
            }
        }

        private bool FightResults(bool gameOver, List<string> userResults, List<string> enemyResults)
        {
            AudioResultsFormatting(userResults);
            AudioResultsFormatting(enemyResults);
            return gameOver;
        }

        private void AudioResultsFormatting(List<string> results)
        {
            try
            {
                string[] audioPieces = results[0].Split("|");
                if (results.Count > 1)
                {
                    string death = results[1];
                    results.Clear();
                    for (int i = 0; i < audioPieces.Length; i++)
                    {
                        results.Add(audioPieces[i]);
                    }
                    results.Add(death);
                }
                else
                {
                    results.Clear();
                    for (int i = 0; i < audioPieces.Length; i++)
                    {
                        results.Add(audioPieces[i]);
                    }
                }
            }
            catch
            {

            }
        }

        private bool FightWithGrunt(UserInput move, Biggie player, Grunt grunt, RoundController rc, List<string> userOutcome, List<string> enemyOutcome)
        {
            //results will be a string containing 3 audio files "classAndMove|HitOrNot|Number
            string results = rc.PlayerTurn(move, grunt, player);
            userOutcome.Add(results);
            if (grunt.isDead())
            {
                userOutcome.Add(CharacterDeath(grunt));
                //the boolean is to see if the game ends The grunt died here so the game does not end
                return false;
            }
            string gruntResult = rc.GruntTurn(grunt, player);
            enemyOutcome.Add(gruntResult);
            if (player.isDead())
            {
                enemyOutcome.Add(CharacterDeath(player));
                //the player died so game ends. Return true;
                return true;
            }
            else return false;
        }

        private bool FightWithBoss(UserInput move, Biggie player, Biggie boss, RoundController rc, List<string> userOutcome, List<string> enemyOutcome)
        {
            //results will be a string containing 3 audio files "classAndMove|HitOrNot|Number
            string results = rc.PlayerTurn(move, boss, player);
            userOutcome.Add(results);
            if (boss.isDead())
            {
                userOutcome.Add(CharacterDeath(boss));
                //the boolean is to see if the game ends The grunt died here so the game does not end
                return false;
            }
            string bossResult = rc.BossTurn(boss, player);
            enemyOutcome.Add(bossResult);
            if (player.isDead())
            {
                enemyOutcome.Add(CharacterDeath(player));
                //the player died so game ends. Return true;
                return true;
            }
            else return false;
        }

        public string ChangeRoom(string userInput)
        {
            //This method expects the string to already be formatted correctly
            //This method will either change current room to the direction connection or it's going to return a message that says there is no room that way
            bool isEnemy = checkIfEnemy();
            if (isEnemy)
            {
                //Check to make sure the user isn't currenlty fighting an enemy
                return "CantRun";
            }
            else
            {
                IMongoDatabase database = client.GetDatabase("GameData");
                var run = database.GetCollection<BsonDocument>("CurrentRun");
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Exists("location");

                BsonValue nextRoom;
                BsonValue roomLevel;
                var doc = run.Find(filter).FirstOrDefault();
                if (doc.TryGetValue(userInput, out nextRoom))
                {
                    //Need the room level to get the correct room from the map
                    doc.TryGetValue("level", out roomLevel);
                    //the TryGetValue method will put the room location that the user is trying to access in nextRoom so I can just set the current room in here
                    var map = database.GetCollection<BsonDocument>("Map");
                    var findRoomFilter = builder.Eq("location", nextRoom) & builder.Eq("level", roomLevel);
                    var newRoom = map.Find(findRoomFilter).FirstOrDefault();
                    run.DeleteOne(filter);
                    run.InsertOne(newRoom);



                    //I need to prepare the room for the player. If there is an enemy it's just going to get the enemy ready. Player always makes the first move
                    //However if it's an item, heals, or no enemy then I have to return the correct files while setting up the room for the player to move again

                    BsonValue enemyClass;
                    newRoom.TryGetValue("enemy", out enemyClass);
                    string roomAudio = "";
                    if (enemyClass.AsString.Equals("none"))
                    {
                        //true is passed because we want the player
                        Biggie player = FetchBiggie("True");
                        roomAudio = DetermineRoomAffect(newRoom, player);
                        UpdateCharacter(player);
                    }
                    else
                    {
                        BsonValue boss;
                        BsonValue newRoomLevel;
                        newRoom.TryGetValue("boss", out boss);
                        newRoom.TryGetValue("level", out newRoomLevel);
                        if (boss.ToBoolean())
                        {

                            ICharacter enemy = CreateNewEnemy(enemyClass.AsString, newRoomLevel.ToInt32());
                            string longName = enemy.GetType().Name;
                            string[] pieces = longName.Split(".");
                            roomAudio = pieces[pieces.Length - 1] + "Room";
                            run.InsertOne(enemy.ToBson());
                        }
                        else
                        {
                            ICharacter enemy = CreateNewEnemy(enemyClass.AsString, newRoomLevel.ToInt32());
                            string longName = enemy.GetType().Name;
                            string[] pieces = longName.Split(".");
                            roomAudio = pieces[pieces.Length - 1] + "Room";
                            run.InsertOne(enemy.ToBson());
                        }
                    }

                    return roomAudio;

                }
                else
                {
                    Console.WriteLine("There is no room to travel to " + userInput + " from here");
                    //this will not be a console line on the server. Ther server will return an audio file that informs the user this information.
                }
                return "NoRoute";
            }

        }

        private bool checkIfEnemy()
        {
            //try to create Grunt and Biggie. If either creates a character return true
            Grunt grunt = FetchGrunt();
            if (grunt != null) return true;
            Biggie boss = FetchBiggie("False");
            if (boss != null) return true;
            return false;
        }

        private string DetermineRoomAffect(BsonDocument newRoom, Biggie player)
        {
            BsonValue heals;
            BsonValue item;
            newRoom.TryGetValue("heals", out heals);
            newRoom.TryGetValue("item", out item);
            if (heals.ToBoolean())
            {
                HealingFountain fountain = new HealingFountain();
                fountain.HealingRoom(player);
                return "FountainRoom";
            }
            else if (item.ToBoolean())
            {
                BsonValue tool;
                newRoom.TryGetValue("tool", out tool);
                string itemSound = "";
                //sadly there will be coupling here
                switch (tool.AsString)
                {
                    case "Goggles":
                        player.ApplyItem("Goggles");
                        itemSound += "GoggleRoom";
                        break;
                    case "Amulet":
                        player.ApplyItem("Amulet");
                        itemSound += "AmuletRoom";
                        break;
                    case "Hat":
                        player.ApplyItem("Hat");
                        itemSound += "HatRoom";
                        break;
                    case "Watch":
                        player.ApplyItem("Watch");
                        itemSound += "WatchRoom";
                        break;
                }
                return itemSound;
            }
            else
            {
                return "OldRoom";
            }
        }

        private UserInput determineUserInput(string userInput)
        {
            switch (userInput)
            {
                case "q":
                    return UserInput.Q;
                case "w":
                    return UserInput.W;
                case "e":
                    return UserInput.E;
                case "a":
                    return UserInput.A;
                case "s":
                    return UserInput.S;
                case "d":
                    return UserInput.D;
                case "u":
                    return UserInput.U;
                default:
                    return UserInput.A;
            }
        }

        private ICharacter CreateNewEnemy(string enemyClass, int level)
        {
            switch (enemyClass)
            {
                case "random":
                    List<Grunt> grunts = new List<Grunt>() { new Sqwaubler(), new GigaWatt(), new WaterGoblin() };
                    return grunts.OrderBy(g => new Random().Next()).ElementAt(0);
                case "JimKin":
                    return new JimKin(level);
                case "InfernalWish":
                    return new InfernalWish(level);
                case "Doggo":
                    return new Doggo(level);
                case "Cowboy":
                    return new Cowboy(level);
                default:
                    //this should never be reached
                    return null;
            }
        }

        private void UpdateGruntDead(Grunt enemy, Biggie user)
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;

            var currentRoomFilter = builder.Exists("location");
            var gruntFilter = builder.Eq("grunt", true);
            var userFilter = builder.Eq("player", "True");

            BsonValue roomGPS;
            BsonValue roomLevel;
            var doc = run.Find(currentRoomFilter).FirstOrDefault();
            if (doc.TryGetValue("location", out roomGPS))
            {
                //Need the room level to get the correct room from the map
                if (doc.TryGetValue("level", out roomLevel))
                {
                    var map = database.GetCollection<BsonDocument>("Map");
                    var findRoomFilter = builder.Eq("location", roomGPS) & builder.Eq("level", roomLevel);
                    var newRoom = map.Find(findRoomFilter).FirstOrDefault();

                    string enemyString = "{ $set: { \"enemy\": \"none\" }}";
                    map.UpdateOne(findRoomFilter, BsonDocument.Parse(enemyString));
                    run.DeleteOne(gruntFilter);
                    run.ReplaceOne(userFilter, user.ToBson());
                }
            }
        }

        private void UpdateBossDead(Biggie enemy, Biggie player)
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;

            var currentRoomFilter = builder.Exists("location");
            var bossFilter = builder.Eq("player", "False");
            var playerFilter = builder.Eq("player", "True");

            BsonValue roomGPS;
            BsonValue roomLevel;
            var doc = run.Find(currentRoomFilter).FirstOrDefault();
            if (doc.TryGetValue("location", out roomGPS))
            {
                //Need the room level to get the correct room from the map
                if (doc.TryGetValue("level", out roomLevel))
                {
                    var map = database.GetCollection<BsonDocument>("Map");
                    int newLevel = roomLevel.AsInt32 + 1;
                    var newRoomFilter = builder.Eq("start", true) & builder.Eq("level", BsonValue.Create(newLevel));
                    var newRoom = map.Find(newRoomFilter).FirstOrDefault();

                    run.DeleteOne(doc);
                    run.InsertOne(newRoom);
                    run.DeleteOne(bossFilter);
                    player.LevelUp();
                    run.ReplaceOne(playerFilter, player.ToBson());
                }
            }
        }

        private void UpdateCharactersGruntDataMongo(Biggie user, Grunt enemy)
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;

            var filter = builder.Eq("grunt", true);
            var userFilter = builder.Eq("player", "True");

            //Console.WriteLine(run.Find(filter).FirstOrDefault().ToString());

            //run.UpdateOne(filter, enemy.ToBson());
            run.ReplaceOne(filter, enemy.ToBson());
            run.ReplaceOne(userFilter, user.ToBson());
        }

        private void UpdateCharacter(Biggie player)
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;

            var userFilter = builder.Eq("player", "True");

            run.ReplaceOne(userFilter, player.ToBson());
        }

        private void UpdateCharactersBossDataMongo(Biggie user, Biggie enemy)
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;

            var filter = builder.Eq("player", "False");
            var userFilter = builder.Eq("player", "True");

            run.ReplaceOne(filter, enemy.ToBson());
            run.ReplaceOne(userFilter, user.ToBson());
        }

        private Grunt FetchGrunt()
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;

            var filter = builder.Eq("grunt", true);

            var doc = run.Find(filter).FirstOrDefault();
            if (doc == null)
            {
                return null;
            }

            BsonValue level;
            BsonValue health;
            BsonValue damage;
            BsonValue dodgeValue;
            BsonArray characterDodge;
            BsonValue block;
            BsonValue accuracyValue;
            BsonArray characterAccuracy;
            BsonValue tact;
            BsonValue tactDuration;
            BsonValue attemptDodge;
            BsonValue attemptBlock;

            doc.TryGetValue("level", out level);
            doc.TryGetValue("health", out health);
            doc.TryGetValue("damage", out damage);
            doc.TryGetValue("dodge", out dodgeValue);
            characterDodge = dodgeValue.AsBsonArray;
            doc.TryGetValue("block", out block);
            doc.TryGetValue("accuracy", out accuracyValue);
            characterAccuracy = accuracyValue.AsBsonArray;
            doc.TryGetValue("tactCooldown", out tact);
            doc.TryGetValue("tactDuration", out tactDuration);
            doc.TryGetValue("attemptDodge", out attemptDodge);
            doc.TryGetValue("attemptBlock", out attemptBlock);

            List<int> dodge = new List<int>();
            List<int> accuracy = new List<int>();
            foreach (BsonValue item in characterDodge)
            {
                dodge.Add(item.ToInt32());
            }
            foreach (BsonValue item in characterAccuracy)
            {
                accuracy.Add(item.ToInt32());
            }
            //first figure out the Grunt and then plug in all the data;

            BsonValue gruntClass;
            doc.TryGetValue("class", out gruntClass);
            string[] peices = gruntClass.AsString.Split(".");
            bool boolBlock = true;
            bool boolDodge = true;
            if (attemptBlock.AsString.Equals("False")) boolBlock = false;
            if (attemptDodge.AsString.Equals("False")) boolDodge = false;
            switch (peices[peices.Length - 1])
            {
                case "GigaWatt":
                    GigaWatt gigaWatt = new GigaWatt(health.ToInt32(), damage.ToInt32(), dodge.ToArray(), block.ToInt32(), accuracy.ToArray(),
                        level.ToInt32(), tact.ToInt32(), tactDuration.ToInt32(), boolBlock, boolDodge);
                    return gigaWatt;
                case "Sqwaubler":
                    Sqwaubler sqwaubler = new Sqwaubler(health.ToInt32(), damage.ToInt32(), dodge.ToArray(), block.ToInt32(), accuracy.ToArray(),
                        level.ToInt32(), tact.ToInt32(), tactDuration.ToInt32(), boolBlock, boolDodge);
                    return sqwaubler;
                case "WaterGoblin":
                    WaterGoblin waterGoblin = new WaterGoblin(health.ToInt32(), damage.ToInt32(), dodge.ToArray(), block.ToInt32(), accuracy.ToArray(),
                        level.ToInt32(), tact.ToInt32(), tactDuration.ToInt32(), boolBlock, boolDodge);
                    return waterGoblin;
                case "JimKin":
                    JimKin jimKin = new JimKin(health.ToInt32(), damage.ToInt32(), dodge.ToArray(), block.ToInt32(), accuracy.ToArray(),
                        level.ToInt32(), tact.ToInt32(), tactDuration.ToInt32(), boolBlock, boolDodge);
                    return jimKin;
                default:
                    throw new EntryPointNotFoundException();
            }

        }

        private Biggie FetchBiggie(string isPlayer)
        {
            IMongoDatabase database = client.GetDatabase("GameData");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("player", isPlayer);

            var doc = run.Find(filter).FirstOrDefault();
            if (doc == null)
            {
                return null;
            }

            BsonValue level;
            BsonValue health;
            BsonValue damage;
            BsonValue dodgeValue;
            BsonArray characterDodge;
            BsonValue block;
            BsonValue accuracyValue;
            BsonArray characterAccuracy;
            BsonValue tact;
            BsonValue util;
            BsonValue ult;
            BsonValue tactDuration;
            BsonValue utilDuration;
            BsonValue ultDuration;
            BsonValue attemptDodge;
            BsonValue attemptBlock;
            BsonValue itemValue;
            BsonArray playerItems = null;

            doc.TryGetValue("level", out level);
            doc.TryGetValue("health", out health);
            doc.TryGetValue("damage", out damage);
            doc.TryGetValue("dodge", out dodgeValue);
            characterDodge = dodgeValue.AsBsonArray;
            doc.TryGetValue("block", out block);
            doc.TryGetValue("accuracy", out accuracyValue);
            characterAccuracy = accuracyValue.AsBsonArray;
            doc.TryGetValue("tactCooldown", out tact);
            doc.TryGetValue("utilCooldown", out util);
            doc.TryGetValue("ultCooldown", out ult);
            doc.TryGetValue("tactDuration", out tactDuration);
            doc.TryGetValue("utilDuration", out utilDuration);
            doc.TryGetValue("ultDuration", out ultDuration);
            doc.TryGetValue("attemptDodge", out attemptDodge);
            doc.TryGetValue("attemptBlock", out attemptBlock);

            List<int> dodge = new List<int>();
            List<int> accuracy = new List<int>();
            foreach (BsonValue item in characterDodge)
            {
                dodge.Add(item.ToInt32());
            }
            foreach (BsonValue item in characterAccuracy)
            {
                accuracy.Add(item.ToInt32());
            }
            List<string> tools = new List<string>();
            if (doc.TryGetValue("items", out itemValue))
            {
                playerItems = itemValue.AsBsonArray;
                foreach (BsonValue item in playerItems)
                {
                    tools.Add(item.ToString());
                }

            }
            //first figure out the PC and then plug in all the data;

            BsonValue playerClass;
            doc.TryGetValue("class", out playerClass);
            string[] peices = playerClass.AsString.Split(".");
            bool boolBlock = true;
            bool boolDodge = true;
            if (attemptBlock.AsString.Equals("False")) boolBlock = false;
            if (attemptDodge.AsString.Equals("False")) boolDodge = false;

            //Here Do If staments of Swithc staments and call the corresponding Method Boss or PLayer and pass in all variables used for constructor
            if (isPlayer.Equals("True")) return CreatePlayer(peices[peices.Length - 1], health.ToInt32(), damage.ToInt32(), dodge.ToArray(), block.ToInt32(), accuracy.ToArray(),
                         level.ToInt32(), tact.ToInt32(), tactDuration.ToInt32(), util.ToInt32(), utilDuration.ToInt32(), ult.ToInt32(),
                         ultDuration.ToInt32(), boolBlock, boolDodge, tools);
            else return CreateBoss(peices[peices.Length - 1], health.ToInt32(), damage.ToInt32(), dodge.ToArray(), block.ToInt32(), accuracy.ToArray(),
                         level.ToInt32(), tact.ToInt32(), tactDuration.ToInt32(), util.ToInt32(), utilDuration.ToInt32(), ult.ToInt32(),
                         ultDuration.ToInt32(), boolBlock, boolDodge, tools);
        }

        private Biggie CreatePlayer(string className, int health, int damage, int[] dodge, int block, int[] accuracy, int level, int tact, int tactDuration, int util, int utilDuration,
            int ult, int ultDuration, Boolean attemptBlock, bool attemptDodge, List<string> tools)
        {
            switch (className)
            {
                case "Tank":
                    Tank tank = new Tank(health, damage, dodge, block, accuracy, level, tact, tactDuration, util, utilDuration, ult, ultDuration,
                        attemptBlock, attemptDodge);
                    if (tools.Count > 0)
                    {
                        foreach (string item in tools)
                        {
                            tank.ApplyItem(item);
                        }
                    }
                    return tank;
                case "Brawler":
                    Brawler brawler = new Brawler(health, damage, dodge, block, accuracy, level, tact, tactDuration, util, utilDuration, ult, ultDuration,
                        attemptBlock, attemptDodge);
                    if (tools.Count > 0)
                    {
                        foreach (string item in tools)
                        {
                            brawler.ApplyItem(item);
                        }
                    }
                    return brawler;
                case "ThrillSeeker":
                    ThrillSeeker thrillSeeker = new ThrillSeeker(health, damage, dodge, block, accuracy, level, tact, tactDuration, util, utilDuration, ult, ultDuration,
                        attemptBlock, attemptDodge);
                    if (tools.Count > 0)
                    {
                        foreach (string item in tools)
                        {
                            thrillSeeker.ApplyItem(item);
                        }
                    }
                    return thrillSeeker;
                default:
                    throw new EntryPointNotFoundException();
            }
        }

        private Biggie CreateBoss(string className, int health, int damage, int[] dodge, int block, int[] accuracy, int level, int tact, int tactDuration, int util, int utilDuration,
            int ult, int ultDuration, Boolean attemptBlock, bool attemptDodge, List<string> tools)
        {
            switch (className)
            {
                case "InfernalWish":
                    InfernalWish infernalWish = new InfernalWish(health, damage, dodge, block, accuracy, level, tact, tactDuration, util, utilDuration, ult, ultDuration,
                        attemptBlock, attemptDodge);
                    if (tools.Count > 0)
                    {
                        foreach (string item in tools)
                        {
                            infernalWish.ApplyItem(item);
                        }
                    }
                    return infernalWish;
                case "Doggo":
                    Doggo doggo = new Doggo(health, damage, dodge, block, accuracy, level, tact, tactDuration, util, utilDuration, ult, ultDuration,
                        attemptBlock, attemptDodge);
                    if (tools.Count > 0)
                    {
                        foreach (string item in tools)
                        {
                            doggo.ApplyItem(item);
                        }
                    }
                    return doggo;
                case "Cowboy":
                    Cowboy cowboy = new Cowboy(health, damage, dodge, block, accuracy, level, tact, tactDuration, util, utilDuration, ult, ultDuration,
                        attemptBlock, attemptDodge);
                    if (tools.Count > 0)
                    {
                        foreach (string item in tools)
                        {
                            cowboy.ApplyItem(item);
                        }
                    }
                    return cowboy;
                default:
                    throw new EntryPointNotFoundException();
            }
        }
        public void StartGame(string userClass)
        {
            Biggie user = new Brawler();
            if (userClass.Equals("Brawler"))
            {
                user = new Brawler();
            }
            else if (userClass.Equals("ThrillSeeker"))
            {
                user = new ThrillSeeker();
            }
            else if (userClass.Equals("Tank"))
            {
                user = new Tank();
            }

            BsonDocument characterForMongo = user.ToBson();

            var client = new MongoClient("mongodb+srv://MatthewC:Vault159@cluster0.2ximt.mongodb.net/GameData?retryWrites=true&w=majority");

            //getting the starting room first
            IMongoDatabase database = client.GetDatabase("GameData");
            var room = database.GetCollection<BsonDocument>("Map");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("start", true) & builder.Eq("level", 1);

            var currentRoom = room.Find(filter).ToList();
            BsonDocument roomData = new BsonDocument();
            currentRoom.ForEach(doc =>
            {
                //there should only be one room but this is the easiest way to access it
                roomData = doc;

            });

            database.DropCollection("CurrentRun");
            database.CreateCollection("CurrentRun");
            var run = database.GetCollection<BsonDocument>("CurrentRun");
            run.InsertOne(characterForMongo);
            run.InsertOne(roomData);
        }

        private string CharacterDeath(ICharacter character)
        {
            //this method just returns the audio name for the character that died
            string characterName = GetCharacterName(character.GetType().Name);
            return characterName + "Die";
        }


        private void Test()
        {
            var client = new MongoClient("mongodb+srv://MatthewC:Vault159@cluster0.2ximt.mongodb.net/GameData?retryWrites=true&w=majority");
            IMongoDatabase database = client.GetDatabase("GameData");
            var map = database.GetCollection<BsonDocument>("Map");
            var documents = map.Find(new BsonDocument()).ToList();
            foreach (BsonDocument doc in documents)
            {
                Console.WriteLine(doc.ToString());
            }
            Console.WriteLine();
        }

        private string GetCharacterName(string characterName)
        {
            string[] namePieces = characterName.Split(".");
            return namePieces[namePieces.Count() - 1];
        }
    }
}
