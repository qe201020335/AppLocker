import os, sys

steam_path = r"C:\Program Files (x86)\Steam\steam.exe"
wsl_steam_path = "/mnt/c/Program Files (x86)/Steam/steam.exe"


def lock():
    with open(steam_path, "rb") as filein:
        with open("data_chunk", "wb") as fileout:
            fileout.write(bytes(40))
            byte = filein.read(1)
            while byte:
                fileout.write(byte)
                byte = filein.read(1)

    os.remove(steam_path)


def release():
    with open(steam_path, "wb") as file_out:
        with open("data_chunk", "rb") as file_in:
            file_in.read(40)
            byte = file_in.read(1)
            while byte:
                file_out.write(byte)
                byte = file_in.read(1)


def test():
    with open("temp", "wb") as file_out:
        s = "Hello this is a string"
        # a = [x%2 for x in range(40)]
        # file_out.write(bytes(a))
        file_out.write(bytes(40))
        # file_out.write(bytearray(s, "utf-8"))
    # with open("temp", "rb") as file_in:
    #     file_in.read(40)
    #     s = str(file_in.read(22), "utf-8")
    # print(s)



def main():
    release()


if __name__ == '__main__':
    main()