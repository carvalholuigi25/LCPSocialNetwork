#!/bin/bash
clear

if [ -f "$HOME\Documents\projects\angular\LCPSocialNetwork\api\LCPSNWebApi\bin" ]; then
	rm -rf "$HOME\Documents\projects\angular\LCPSocialNetwork\api\LCPSNWebApi\bin"
fi

if [ -f "$HOME\Documents\projects\angular\LCPSocialNetwork\api\LCPSNWebApi\obj" ]; then
	rm -rf "$HOME\Documents\projects\angular\LCPSocialNetwork\api\LCPSNWebApi\obj"
fi

echo Cleaned up all files from obj and bin directories!

exit