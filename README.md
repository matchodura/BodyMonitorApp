## Body Monitor App
* [General info](#general-info)
* [Features](#features)
* [Technologies](#technologies)
* [Executable](#executable)
* [Application](#Application)
* [Setup](#setup)

## General info
Body Monitor App is a desktop application created for Windows Systems. It was made using Windows Presentation Foundation(WPF) technology in MVVM pattern. The main purpose of the application is to provide user a tool, which can be used to record and display values of specific body parts - Neck, Chest, Stomach, Waist, Thigh, Calf, Biceps and Weight. Inserted data can be edited and displayed in any time. 

## Features
* Creating and updating user profile with full validation of input data
* New password can be set up using secret question-answer option
* Adding records of user body values for specific time and editing them
* Displaying values on time based chart with range of 1 year, 30 and 7 days
* Providing catalogue of exercises for selected body parts via youtube video
* Application uses local database, making it fully offline 

## Technologies
Project was created with:
* WPF with MaterialDesing with XAML and Livecharts libraries
* .NET Framework 4.7.2
* C# 8.0
* ADO.NET
* MS SQL Server 18 when database is stored on server
* MS SQL Compact Edition 4.0 when database is stored as local file



## Executable
To see app in action you can download installer from link provided below:

[Download](https://drive.google.com/drive/folders/17IuH4vH-SOdCPukadqKCXFuXIeP5fz8_?usp=sharing)
## Application

(gifs may take a while to load)

Main page presents login screen and options to create account and forgot password tool.

![](https://drive.google.com/uc?export=view&id=10jYwsrYQe03qtoVH-mLwi3u2qOBEkj7H)

On account creation page user provides credentials with additional data like birthday, height or e-mail. Also few secret questions options are avaliable to help with setting up a new password when old one was forgotten. Users passwords are hashsalted in database.

![](https://drive.google.com/uc?export=view&id=1gV2Bt7KbCfJ8s0trE00oANnG8ymriiUL)

When user forgots password, it is possible to set a new one with providing valid answer to secret question created when registering.

![](https://drive.google.com/uc?export=view&id=1UU-RiABhM8vY-m17p91P7DrnEYdN-byK)

User logs in with login and provided on registration password.

![](https://drive.google.com/uc?export=view&id=13_ItYiWDy5ptzK3L8wsMYme8847KbV1z)

After logging to the app user can see navigation panel on the top of screen and dashboard presenting last inserted weight, current BMI and select interesting body parts to see its latest value.

![](https://drive.google.com/uc?export=view&id=1RnstkqbXSxYqRGP_8AKeA44jGZB-r300)

About page provides links to author's pages.

![](https://drive.google.com/uc?export=view&id=1Ux7J-Gx9tuImRv3Jr03-rDYNNMGM9p-G)

Workouts are a catalogue of exercises for specific body parts. User can navigate between them or select set for interesting body part. All videos are links to professionals presenting correct way of doing them.

![](https://drive.google.com/uc?export=view&id=1V5YDy1RL2rR5f_WPEmJvEXm1zjpSQwm7)

User has some options to edit profile data - name, height, birthday, e-mail and gender.

![](https://drive.google.com/uc?export=view&id=12CnyjHbOTbMyDy_8tfJJh7zD_ByXfH7D)

To add a value for specific body part, user first selects option of adding it, then chooses interesting date. Clicking edit button enables option to edit data. When values are provided records can be added to database.

![](https://drive.google.com/uc?export=view&id=16bcgvC5w8qzpKNPNHf4GoaHBGJPjumAu)

Selecting option of editing values and choosing date when such records exist, user can edit them and update the database. If no values are provided prior to editing add button is displayed for user to suggest it is a new record.

![](https://drive.google.com/uc?export=view&id=1G3m-aJmkJXFe1pBLf1ti8uInFYhCXbmN)

Charts enable user to see progress of every parameter on specific timespan - one year, 7 and 30 days in the past. 

![](https://drive.google.com/uc?export=view&id=1l8OTC94zX4BOc9YGL19rKan-MQwlMRMw)

When user stops using of application he can logout and come back later. All values are stored in local database existing on user's Personal Computer.

![](https://drive.google.com/uc?export=view&id=1smEAQ3Yk3G0RmJ7EUQHleRjXI21u3vSi)



	
## Setup

There are two ways to run the application:

First way - running executable
1. Download zip with application from link below:

    [Link](https://drive.google.com/drive/folders/17IuH4vH-SOdCPukadqKCXFuXIeP5fz8_?usp=sharing)

2. Unzip contents to desired folder
3. Run setup.exe, accept installation of .NET framework if needed4.
4. When finished installing program starts automatically, after closing you can run the app from BodyMonitorApp.application
5. Happy using!

Second way - running project
1. Ensure .NET is installed
2. Clone or download repository to desired folder
3. Choose way of opening the application
    1. Open  solution in Visual Studio
    2. Navigate to folder with command prompt and write
    ```
    > C:\BodyMonitorApp>dotnet run
    ```
4. Database can be created with sql scripts that are contained in DatabaseBodyMonitor directory. ConnectionString can be setup in helper class:
```
Helpers\Connections.cs 
```
5. Run and enjoy!
