import base64
import logging
import sys
from signalrcore.hub_connection_builder import HubConnectionBuilder
from pydub import AudioSegment
from pydub.playback import play
import os


def play_message(msg):
    # print(msg)
    encoded_msg = msg[0]
    message_byte = base64.b64decode(encoded_msg)
    m=[]
    for i in message_byte:
         m.append(i)
    binary_format = bytearray(m)
    f = open("testing.wav", 'w+b')
    f.write(binary_format)
    f.close
    sound = AudioSegment.from_wav("testing.wav")
    play(sound)


def input_with_default(input_text, default_value):
    value = input(input_text.format(default_value))
    return default_value if value is None or value.strip() == "" else value


server_url = input_with_default('Enter your server url(default: {0}): ', "wss://localhost:5001/chatHub")
username = input_with_default('Enter your username (default: {0}): ', "mandrewcito")
handler = logging.StreamHandler()
handler.setLevel(logging.DEBUG)
hub_connection = HubConnectionBuilder()\
    .with_url(server_url, options={"verify_ssl": False}) \
    .configure_logging(logging.DEBUG, socket_trace=True, handler=handler) \
    .with_automatic_reconnect({
            "type": "interval",
            "keep_alive_interval": 10,
            "intervals": [1, 3, 5, 6, 7, 87, 3]
        }).build()

hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
hub_connection.on_close(lambda: print("connection closed"))

# hub_connection.on("RecieveAudio", play_message)
hub_connection.on("ReceiveMessage", print)

hub_connection.start()


message = None

# Do login

# while message != "exit()":
#     message = input(">> ")
#     if message is not None and message != "" and message != "exit()":
#         hub_connection.send("SendAudio", [])
#         print("I'M SENDING HERE")
#         print("I'M SENDING HERE")
#         print("I'M SENDING HERE")
#     else:
#         try:
#             os.remove("testing.wav")
#         finally:
#             print("")

while message != "exit()":
    message = input(">> ")
    if message is not None and message != "" and message != "exit()":
        hub_connection.send("SendMessage", [username, message])
        print("I'M SENDING HERE")


hub_connection.stop()




sys.exit(0)