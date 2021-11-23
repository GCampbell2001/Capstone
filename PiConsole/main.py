import base64
import logging
import sys
from signalrcore.hub_connection_builder import HubConnectionBuilder
from pydub import AudioSegment
from pydub.playback import play
import simpleaudio as sa
import os
import keyboard


# def play_message(msg):
#     # print(msg)
#     encoded_msg = msg[0]
#     message_byte = base64.b64decode(encoded_msg)
#     m=[]
#     for i in message_byte:
#          m.append(i)
#     binary_format = bytearray(m)
#     f = open("testing.wav", 'w+b')
#     f.write(binary_format)
#     f.close
#     sound = sa.WaveObject.from_wave_file("testing.wav")
#     play_obj = sound.play()
#     play_obj.wait_done()
#     # print("This shouldn't print until after file is done playing")
#     os.remove("testing.wav")
#     # play(sound)
#
#
# def input_with_default(input_text, default_value):
#     value = input(input_text.format(default_value))
#     return default_value if value is None or value.strip() == "" else value
#
#
# server_url = input_with_default('Enter your server url(default: {0}): ', "wss://localhost:5001/chatHub")
# username = input_with_default('Enter your username (default: {0}): ', "mandrewcito")
# handler = logging.StreamHandler()
# handler.setLevel(logging.DEBUG)
# hub_connection = HubConnectionBuilder()\
#     .with_url(server_url, options={"verify_ssl": False}) \
#     .configure_logging(logging.DEBUG, socket_trace=True, handler=handler) \
#     .with_automatic_reconnect({
#             "type": "interval",
#             "keep_alive_interval": 10,
#             "intervals": [1, 3, 5, 6, 7, 87, 3]
#         }).build()
#
# hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
# hub_connection.on_close(lambda: print("connection closed"))
#
#
# hub_connection.start()
#
# hub_connection.on("RecieveAudio", play_message)
# #hub_connection.on("ReceiveMessage", print)
#
#
#
#
# message = None
#
# # Do login
#
# while message != "exit()":
#     message = input(">> ")
#     if message is not None and message != "" and message != "exit()":
#         hub_connection.send("SendAudio", [])
#         print("I'M SENDING HERE")
#         print("I'M SENDING HERE")
#         print("I'M SENDING HERE")
#     else:
#         try:
#             # os.remove("testing.wav")
#             print("python is angy and this fixes it. lil *****")
#         finally:
#             print("line72")
#
# # while message != "exit()":
# #     message = input(">> ")
# #     if message is not None and message != "" and message != "exit()":
# #         hub_connection.send("SendMessage", [username, message])
# #         print("I'M SENDING HERE")
#
#
# hub_connection.stop()

def play_game():
    # server_url = "wss://localhost:5001/chatHub"
    # # username = input_with_default('Enter your username (default: {0}): ', "mandrewcito")
    # handler = logging.StreamHandler()
    # handler.setLevel(logging.DEBUG)
    # hub_connection = HubConnectionBuilder() \
    #     .with_url(server_url, options={"verify_ssl": False}) \
    #     .configure_logging(logging.DEBUG, socket_trace=True, handler=handler) \
    #     .with_automatic_reconnect({
    #     "type": "interval",
    #     "keep_alive_interval": 10,
    #     "intervals": [1, 3, 5, 6, 7, 87, 3]
    # }).build()
    #
    # hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
    # hub_connection.on_close(lambda: print("connection closed"))
    #
    # hub_connection.start();

    #TODO:          play intro audio.
    #TODO:          play character Intro Dialogues
    #TODO:          Need to either set it up so you can press up and down to navigate choices and then press enter to choose or make it key specific

    #TODO:          Read UserInput KeyPress and make calls to methods based of this.

    #skipping intro and going right into Intro Dialogues
    need_to_make_choice = True
    while need_to_make_choice:
        current_choice = 0
        current_choice = Determine_Character_Choice()
        if current_choice < 4 and current_choice > 0:
            print(current_choice)
            need_to_make_choice = False
        # sound_reg = sa.WaveObject.from_wave_file("ReginaldIntro.wav")
        # sound_bel = sa.WaveObject.from_wave_file("BelladonnaIntro.wav")
        # sound_nodge = sa.WaveObject.from_wave_file("NodgeIntro.wav")
        # play_reg = sound_reg.play()
        # if(keyboard.is_pressed('down')):
        #     play_reg.stop()
        #     play_bell = sound_bel.play()






#    hub_connection.stop();


def Determine_Character_Choice():
    sound_reg = sa.WaveObject.from_wave_file("ReginaldIntro.wav")
    play_reg = sound_reg
    return reginald_choice(play_reg)


def reginald_choice(reg):
    #this is choice 1
    key_not_pressed = True
    sound_reg = sa.WaveObject.from_wave_file("ReginaldIntro.wav")
    print("HitReggie")
    play_reg = sound_reg.play()
    play_reg.wait_done()
    while key_not_pressed:
        if keyboard.is_pressed('down'):
            return belladonna_choice(play_reg)
        if keyboard.is_pressed('enter'):
            return 1


def belladonna_choice(reg):
    #this is choice 2
    key_not_pressed = True
    sound_bell = sa.WaveObject.from_wave_file("BelladonnaIntro.wav")
    print("htiBellodonna")
    play_bell = sound_bell.play()
    play_bell.wait_done()
    while key_not_pressed:
        if keyboard.is_pressed('down'):
            return nodge_choice(play_bell)
        if keyboard.is_pressed('enter'):
            return 2

def nodge_choice(reg):
    key_not_pressed = True
    sound_nodg = sa.WaveObject.from_wave_file("NodgeIntro.wav")
    print("HitNodge")
    play_nodg = sound_nodg.play()
    play_nodg.wait_done()
    while key_not_pressed:
        if keyboard.is_pressed('down'):
            return reginald_choice(play_nodg)
        if keyboard.is_pressed('enter'):
            return 3


play_game()
sys.exit(0)