import base64
import logging
import sys

import keyboard
import pynput.keyboard
from signalrcore.hub_connection_builder import HubConnectionBuilder
from pydub import AudioSegment
from pydub.playback import play
import simpleaudio as sa
import os
from pynput import keyboard

server_url = "wss://localhost:5001/chatHub"
# username = input_with_default('Enter your username (default: {0}): ', "mandrewcito")
handler = logging.StreamHandler()
handler.setLevel(logging.DEBUG)
hub_connection = HubConnectionBuilder() \
        .with_url(server_url, options={"verify_ssl": False}) \
        .configure_logging(logging.DEBUG, socket_trace=True, handler=handler) \
        .with_automatic_reconnect({
        "type": "interval",
        "keep_alive_interval": 10,
        "intervals": [1, 3, 5, 6, 7, 87, 3]
    }).build()

# this is the baseline for the character selection




def play_game():


    hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
    hub_connection.on_close(lambda: print("connection closed"))
    #
    hub_connection.start()

    # TODO:          play intro audio.
    # TODO:          play character Intro Dialogues
    # TODO:          Need to either set it up so you can press up and down to navigate choices and then press enter to choose or make it key specific

    # TODO:          Read UserInput KeyPress and make calls to methods based of this.

    # skipping intro and going right into Intro Dialogues
    # need_to_make_choice = True
    # player_alive = True
    # current_choice = 0
    # while need_to_make_choice:
    #     current_choice = determine_character_choice()
    #     if current_choice < 4 and current_choice > 0:
    #         print(current_choice)
    #         need_to_make_choice = False
    # hub_connection.send("StartGame", [get_character_class(current_choice)])
    # this get's the menu to start playing
    # player_menu(1)
    # with keyboard.Listener(on_press=on_press, on_release=on_release_menu) as listener:
    #     listener.join()
    hub_connection.send("StartGame", [get_character_class(3)])
    with keyboard.Listener(on_press=on_press, on_release=on_release) as listener:
        listener.join()



def on_press(key):
    #nothing.
    print()


def on_release(key):
    try:
        if key.char == ('a'):
            action_attack(hub_connection)
        elif key.char == ('s'):
            action_block(hub_connection)
        elif key.char == ('d'):
            action_dodge(hub_connection)
        elif key.char == ('q'):
            action_tactical(hub_connection)
        elif key.char == ('w'):
            action_utility(hub_connection)
        elif key.char == ('e'):
            action_ultimate(hub_connection)
        elif key.char == ('u'):
            update(hub_connection)
    except AttributeError:
        print()
        # it will try to do those but if something like the up arrow is pressed and error will occur.
        # not an actual problem though, so we'll just ignore it
    if key == pynput.keyboard.Key.up:
        traverse_north(hub_connection)
    elif key == pynput.keyboard.Key.down:
        traverse_south(hub_connection)
    elif key == pynput.keyboard.Key.left:
        traverse_west(hub_connection)
    elif key == pynput.keyboard.Key.right:
        traverse_east(hub_connection)

def action_attack(hub_connection):
    hub_connection.send("Fight", ['a'])
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception at User Attack")
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception at EnemyTurn Attack")


def action_block(hub_connection):
    hub_connection.send("Fight", ['s'])
    try:
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Error at User Block")
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Error at EnemyTurn Block")


def action_dodge(hub_connection):
    hub_connection.send("Fight", ['d'])
    try:
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Excpetion at UserDodge")
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Excpetion at EnemyTurnDodge")


def action_tactical(hub_connection):
    hub_connection.send("Fight", ['q'])
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception UserTacitical")
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception at EnemyTurn Tactical")


def action_utility(hub_connection):
    hub_connection.send("Fight", ['w'])
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception at User Utility")
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception EnemyTurn Utility")


def action_ultimate(hub_connection):
    hub_connection.send("Fight", ['e'])
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception at User Ultimate")
    try:
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
        hub_connection.on("ReceiveFile", room_audio)
    except:
        print("Exception at EnemyTurn Ultimate")


def update(hub_connection):
    hub_connection.send("Update", ['u'])
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)
    hub_connection.on("ReceiveFile", room_audio)


def traverse_north(hub_connection):
    hub_connection.send("ChangeRooms", ['north'])
    hub_connection.on("ReceiveFile", room_audio)


def traverse_south(hub_connection):
    hub_connection.send("ChangeRooms", ['south'])
    hub_connection.on("ReceiveFile", room_audio)


def traverse_west(hub_connection):
    hub_connection.send("ChangeRooms", ['west'])
    hub_connection.on("ReceiveFile", room_audio)


def traverse_east(hub_connection):
    hub_connection.send("ChangeRooms", ['east'])
    hub_connection.on("ReceiveFile", room_audio)


def action_audio(msg):
    encoded_msg = msg[0]
    message_byte = base64.b64decode(encoded_msg)
    m = []
    for i in message_byte:
        m.append(i)
    binary_format = bytearray(m)
    f = open("action.wav", 'w+b')
    f.write(binary_format)
    f.close
    sound = sa.WaveObject.from_wave_file("action.wav")
    play_obj = sound.play()
    play_obj.wait_done()
    # print("This shouldn't print until after file is done playing")
    os.remove("action.wav")


def room_audio(msg):
    #    print(msg)
    encoded_msg = msg[0]
    message_byte = base64.b64decode(encoded_msg)
    m = []
    for i in message_byte:
        m.append(i)
    binary_format = bytearray(m)
    f = open("room.wav", 'w+b')
    f.write(binary_format)
    f.close
    sound = sa.WaveObject.from_wave_file("room.wav")
    play_obj = sound.play()
    play_obj.wait_done()
    # print("This shouldn't print until after file is done playing")
    os.remove("room.wav")


def get_character_class(choice):
    if choice == 1: return "Tank"
    elif choice == 2: return "ThrillSeeker"
    elif choice == 3: return "Brawler"


# def determine_character_choice():
#     # sound_reg = sa.WaveObject.from_wave_file("ReginaldIntro.wav")
#     # play_reg = sound_reg
#     return nodge_choice()


def on_release_menu(key):
    player_choice = 1

    if key == pynput.keyboard.Key.up:
        if player_choice == 3:
            player_choice = 1
        else:
            player_choice += 1
        player_menu(player_choice)
    elif key == pynput.keyboard.Key.down:
        if player_choice == 1:
            player_choice = 3
        else:
            player_choice -= 1
        player_menu(player_choice)
    elif key == pynput.keyboard.Key.enter:
        if player_choice == 0:
            #           player has to make a choice. So this is a fail safe to make sure a choice is made
            print()
        else:
            hub_connection.send("StartGame", [get_character_class(player_choice)])
            return False


def player_menu(player_choice):
    if player_choice == 1:
        nodge_choice()
    if player_choice == 2:
        reginald_choice()
    if player_choice == 3:
        belladonna_choice()


def reginald_choice():
    # this is choice 2
    sound_reg = sa.WaveObject.from_wave_file("ReginaldIntro.wav")
    print("HitReggie")
    play_reg = sound_reg.play()
    play_reg.wait_done()


def belladonna_choice():
    # this is choice 3
    sound_bell = sa.WaveObject.from_wave_file("BelladonnaIntro.wav")
    print("htiBellodonna")
    play_bell = sound_bell.play()
    play_bell.wait_done()


def nodge_choice():
    sound_nodg = sa.WaveObject.from_wave_file("NodgeIntro.wav")
    print("HitNodge")
    play_nodg = sound_nodg.play()
    play_nodg.wait_done()


play_game()
sys.exit(0)
