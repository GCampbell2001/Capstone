import base64

import mysql.connector
import logging
import sys
from pydub import AudioSegment
from pydub.playback import play
import simpleaudio as sa
import numpy as np
import io
import os
import array
from signalrcore.hub_connection_builder import HubConnectionBuilder


def get_audio_location(msg):
    mydb = mysql.connector.connect(
        host="localhost",
        user="rasp",
        password="Yoru^558"
    )
    query_string = "SELECT location FROM audio.files WHERE filename LIKE '" + "1" + "'"
    my_cursor = mydb.cursor(prepared=True)
    my_cursor.execute(query_string)
    print("THIS IS WHERE THE RESULT SHOULD PRINT-f")
    result = my_cursor.fetchone()
    # bytes = open(result[0], "rb").read()
    # byte_array = array.array('B')
    # audio_file = open(result[0], 'rb')
    # byte_array.fromstring(audio_file.read())
    # test_array = byte_array
    with open(result[0], 'rb') as fd:
        contents = fd.read()
    # message_byte = base64.b64decode(contents)
    # m=[]
    # for i in message_byte:
    #      m.append(i)
    # binary_format = bytearray(m)
    f = open("testing.wav", 'w+b')
    f.write(contents)
    f.close
    sound = sa.WaveObject.from_wave_file("testing.wav")
    play_obj = sound.play()
    play_obj.wait_done()
    #test = bytearray(contents, 'utf-8')
    hub_connection.send("SendFile", [contents])








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

hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
hub_connection.on_close(lambda: print("connection closed"))
#

hub_connection.on("ReceiveRequest", get_audio_location)
hub_connection.start()
message = None

# Do login

while message != "exit()":
    message = input(">> ")
    if message is not None and message != "" and message != "exit()":
        hub_connection.send("SendTest", [message])

hub_connection.stop()

sys.exit(0)


