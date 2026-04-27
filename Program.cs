// Docker Sql Server Azure Studio Maker 
using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
class Program
{
    public static string HostName()
    {
        Console.WriteLine("Enter your Name/HostName");
#pragma warning disable CS8603 // Possible null reference return.
        return Console.ReadLine();
#pragma warning restore CS8603 // Possible null reference return.
    }
    public static string Password()
    {
        Console.WriteLine("Enter Your Sql Password (Please Make sure your password has atleast 1 Capital letter and a series of numbers plus a symbol and it's more than 8 characters long)");
#pragma warning disable CS8603 // Possible null reference return.
        return Console.ReadLine();
#pragma warning restore CS8603 // Possible null reference return.
    }
    public static string YourPort()
    {
        Console.WriteLine("Enter Your 4 Digit Port");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string portnumber = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8603 // Possible null reference return.
        return portnumber;
#pragma warning restore CS8603 // Possible null reference return.
    }
    public static void Main()
    {
        string Pattern = @"[Aa-zZ0-9][^Aa-zZ0-9]";
        Regex rg = new Regex(Pattern);
        string HName = HostName();
        string pw = Password();

        if (Convert.ToString(HName) == "")
        {
            Console.Error.WriteLine("Please Enter An Actual Name For The Server.");
            Main();
        }
        else if (rg.IsMatch(Convert.ToString(pw)) & Convert.ToString(pw).Length >= 8 & Convert.ToString(pw).Any(char.IsUpper))
        {
            string pm = YourPort();

            if (Convert.ToString(pm).Length >= 4 && Convert.ToString(pm).Length <= 4)
            {
                // Console.WriteLine($"\nHostName is : {HName}\n Password is : {pw}\n Port is : {pm}");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start("cmd.exe", $"/c docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD={pw}' -p {pm}:1433 --name {HName} --hostname {HName} -d mcr.microsoft.com/mssql/server:2022-latest");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("/bin/bash", $"-c \"docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD={pw}' -p {pm}:1433 --name {HName} --hostname {HName} -d mcr.microsoft.com/mssql/server:2022-latest\"");
                }
                else
                {
                    Console.WriteLine("I didn't add support for macies");
                }
            }
            else
            {
                Console.Error.WriteLine("Please Enter A Port That's 4 Digits");
                YourPort();
            }
        }
        else
        {
            Console.Error.WriteLine("Please Make sure your password has atleast 1 Capital letter and a series of numbers plus a symbol and it's more than 8 characters long");
            Password();
        }
    }
}

/*
 * ^    -   Starts with
 * $    -   Ends with
 * []   -   Range
 * ()   -   Group
 * .    -   Single character once
 * +    -   Matches one or more occurrences of the preceding element.
 *          It requires at least one occurrence for a match.
 * *    -   Matches zero or more occurrences of the preceding element.
 *          It can match zero occurrences, meaning the element is optional.
 * ?    -   optional preceding character match
 * ?=   -   Positive lookahead assertion. It is used to assert that a certain pattern can be
 *          matched ahead in the string without consuming any characters.
 *          This means it checks for the presence of a pattern but does not include it in the match.
 * \    -   escape character
 * \n   -   New line
 * \d   -   Digit
 * \D   -   Non-digit
 * \s   -   White space
 * \S   -   non-white space
 * \w   -   alphanumeric/underscore character (word chars)
 * \W   -   non-word characters
 * {x,y} -  Repeat low (x) to high (y) (no "y" means at least x, no ",y" means exactly x)
 * (x|y) -  Alternative - x or y
 * [^x]  -  Anything but x (where x is whatever character you want)
 */