## ⚠️ help needed, check ip2geo/Program.cs todos ⚠️

# tools

these are just some projects i do while im bored, im still learning c# and experimenting with whatever i know or will know.

PS: this tool is not for skids just because, well i hate them!

(even though im 14 but i make my own shit, sometimes i copy paste and fix whatevers not working but thats besides the point)

# compiling from src (linux)

ive only tested this on debian because thats what i use on a daily basis, so if anyone would be kind enough to make instructions for windows that would be great.

## requirements:

- [.net 8 or newer](https://download.visualstudio.microsoft.com/download/pr/4a252cd9-d7b7-41bf-a7f0-b2b10b45c068/1aff08f401d0e3980ac29ccba44efb29/dotnet-sdk-8.0.300-linux-x64.tar.gz)
  
- any c# ide *(optional as you can build with the* `dotnet publish [args]` *command)*
  

1. clone the repo: `git clone https://github.com/ChainDev162/.net8-projects.git && cd .net8-projects && git checkout master`
  
2. edit the **.csproj** files (*or just uncomment line 8 for* `ip2geo/ip2geo.csproj`*, line 9 for*`login2token/login2token.csproj`, *for windows exes.*)
  
3. finally, run `dotnet publish`, if you edited the files it'll build the exes, the output should be something like this:![](https://github.com/ChainDev162/dotnet8-projects/blob/master/2024-05-31-17-41-53-image.png)
  
  that means that its been built successfully, now just run the exes in the mentioned directories
