import json
import os

from cryptography.fernet import Fernet

APPDATAPATH = os.getenv('APPDATA') + "\\NIKKIT\\"

def createKey():
    key = Fernet.generate_key()
    print("Creating Key")

    with open(APPDATAPATH + 'Credentials.key', 'wb') as mykey:
        mykey.write(key)

def loadKey():
    print("Loading Key")
    with open(APPDATAPATH + 'Credentials.key', 'rb') as mykey:
        key = mykey.read()

    print(key)
    return key

def encrypt(key):
    f = Fernet(key)
    print("Encrypting")

    with open(APPDATAPATH + 'credentials.json', 'rb') as original_file:
        original = original_file.read()

    encrypted = f.encrypt(original)

    with open( APPDATAPATH + 'credentials.JSON', 'wb') as encrypted_file:
        encrypted_file.write(encrypted)

def decrypt(key):
    print("Decrypting")
    f = Fernet(key)

    with open(APPDATAPATH + 'credentials.json', 'rb') as encrypted_file:
        encrypted = encrypted_file.read()

    decrypted = f.decrypt(encrypted)

    with open(APPDATAPATH + 'credentials.json', 'wb') as decrypted_file:
        decrypted_file.write(decrypted)


def createNewCredential(isEncrypted):
    exists = os.path.exists(APPDATAPATH)
    if (exists):

        directory = "NIKKIT"

        # Parent Directory path
        parent_dir = os.getenv('APPDATA')

        # Path
        path = os.path.join(parent_dir, directory)
        mode = 0o666
        os.mkdir(path, mode)
        print("Directory '% s' created" % directory)

    username = str(input("Enter Username"))
    password = str(input("Enter password"))

    data = {
        "usr": username,
        "pwd": password,
    }

    with open(APPDATAPATH + "credentials.json", "w") as f:
        # Write the data to the file as JSON
        json.dump(data, f)

    print("Data written to file.")

    if (isEncrypted):
        createKey()
        key = loadKey()
        encrypt(key)

def main():
    while (True):
        option = int(input("(1) Create New Credential (2) CNC (Unencrypt) (3) Decrypt (4) Encrypt (5) Exit"))

        if (option == 1): createNewCredential(True)
        if (option == 2): createNewCredential(False)
        if (option == 3):
            key = loadKey()
            decrypt(key)
        if (option == 4):
            key = loadKey()
            encrypt(key)
        if(option == 5): break



main()