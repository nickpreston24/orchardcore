#!/bin/bash
# Run this only on Linux distros

## Remove everything .net related
sudo snap remove dotnet-sdk
sudo apt remove 'dotnet*'
sudo apt remove 'aspnetcore*'
sudo apt remove 'netstandard*'
sudo rm /etc/apt/sources.list.d/microsoft-prod.list
sudo rm /etc/apt/sources.list.d/microsoft-prod.list.save

## update all your machine packages
sudo apt update

## install dotnet
sudo apt install dotnet8


