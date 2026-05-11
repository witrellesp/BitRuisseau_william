# BitRuisseau

BitRuisseau is a **C# Windows Forms media library application** that allows users to manage local media files and share the availability of their media library over a network using **MQTT**.

The application was developed as a structured software project with a graphical interface, local media management, network communication and unit tests.

## Project Goal

The goal of this project is to create a small distributed media library system.

Each user can manage their own local media library, make it available online, and discover other media libraries connected to the same network through MQTT messages.

## Main Features

- Add local media files to a personal media library
- Supported file types:
  - MP3
  - MP4
  - MOV
  - GIF
  - PNG
  - JPEG / JPG
  - WAV
- Display local media files in the application
- Remove selected media from the local list
- Play MP3 files directly from the application
- Open other media files with the default system application
- Set the local media library as online or offline
- Send media library status over the network
- Receive and display remote media libraries
- Exchange messages using MQTT
- Basic unit tests

## Tech Stack

- C#
- Windows Forms
- .NET
- MQTT
- JSON serialization
- Windows Media Player library
- Unit testing

## How It Works

The application manages a local media library represented by a `Mediatheque` object.

Each media file is represented by a `Media` object containing:

- file name
- file size
- media type

When the application starts, it connects to an MQTT broker and sends a media status request to discover other available media libraries on the network.

The application can handle two main types of network messages:

- `MEDIA_STATUS_REQUEST`: asks other nodes to send their media library status
- `MEDIA_STATUS`: sends the current media library information as serialized JSON

When another media library is received, it is displayed in the remote media libraries list.

## Project Structure

```bash
BitRuisseau_william/
├── app/
│   ├── Backend/
│   ├── BitRuisseau_william/
│   └── BitRuisseau_williamTests/
├── doc/
│   ├── livrables/
│   ├── maquettes/
│   └── notes/
└── README.md
````

Main Classes
Media

Represents a media file added to the library.

It stores the file name, size and media type.

Mediatheque

Represents a media library.

It contains:

a display name
an availability status
a list of media files
Form1

Main Windows Forms interface.

It handles:

media selection
media display
playback
online/offline status
MQTT communication
remote media library updates
Installation

Clone the repository:

git clone https://github.com/witrellesp/BitRuisseau_william.git
cd BitRuisseau_william

Open the solution file in Visual Studio:

app/BitRuisseau_william/BitRuisseau_william.sln

Restore NuGet packages if necessary, then build and run the project from Visual Studio.

Requirements
Windows
Visual Studio
.NET SDK compatible with the project
Internet or network access to connect to the MQTT broker
What This Project Demonstrates

This project demonstrates my ability to:

Build a desktop application with C# and Windows Forms
Structure a project with separate application, backend and test folders
Work with object-oriented programming
Manage local files through a graphical interface
Use JSON serialization
Communicate over a network using MQTT
Handle basic media playback
Write and organize unit tests
Document a software project
Status

This project is a school/software development project and may still contain features planned for future improvement, such as downloading media from remote libraries.

Author

William Trelles
