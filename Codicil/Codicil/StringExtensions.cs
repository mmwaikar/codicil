using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Codicil
{
    public static class StringExtensions
    {
        private static readonly string[][] TimeZones =
        {
            new[] {"ACDT", "+1030", "Australian Central Daylight"},
            new[] {"ACST", "+0930", "Australian Central Standard"},
            new[] {"ADT", "-0300", "(US) Atlantic Daylight"},
            new[] {"AEDT", "+1100", "Australian East Daylight"},
            new[] {"AEST", "+1000", "Australian East Standard"},
            new[] {"AHDT", "-0900", ""},
            new[] {"AHST", "-1000", ""},
            new[] {"AST", "-0400", "(US) Atlantic Standard"},
            new[] {"AT", "-0200", "Azores"},
            new[] {"AWDT", "+0900", "Australian West Daylight"},
            new[] {"AWST", "+0800", "Australian West Standard"},
            new[] {"BAT", "+0300", "Bhagdad"},
            new[] {"BDST", "+0200", "British Double Summer"},
            new[] {"BET", "-1100", "Bering Standard"},
            new[] {"BST", "-0300", "Brazil Standard"},
            new[] {"BT", "+0300", "Baghdad"},
            new[] {"BZT2", "-0300", "Brazil Zone 2"},
            new[] {"CADT", "+1030", "Central Australian Daylight"},
            new[] {"CAST", "+0930", "Central Australian Standard"},
            new[] {"CAT", "-1000", "Central Alaska"},
            new[] {"CCT", "+0800", "China Coast"},
            new[] {"CDT", "-0500", "(US) Central Daylight"},
            new[] {"CED", "+0200", "Central European Daylight"},
            new[] {"CET", "+0100", "Central European"},
            new[] {"CST", "-0600", "(US) Central Standard"},
            new[] {"CENTRAL", "-0600", "(US) Central Standard"},
            new[] {"EAST", "+1000", "Eastern Australian Standard"},
            new[] {"EDT", "-0400", "(US) Eastern Daylight"},
            new[] {"EED", "+0300", "Eastern European Daylight"},
            new[] {"EET", "+0200", "Eastern Europe"},
            new[] {"EEST", "+0300", "Eastern Europe Summer"},
            new[] {"EST", "-0500", "(US) Eastern Standard"},
            new[] {"EASTERN", "-0500", "(US) Eastern Standard"},
            new[] {"FST", "+0200", "French Summer"},
            new[] {"FWT", "+0100", "French Winter"},
            new[] {"GMT", "-0000", "Greenwich Mean"},
            new[] {"GST", "+1000", "Guam Standard"},
            new[] {"HDT", "-0900", "Hawaii Daylight"},
            new[] {"HST", "-1000", "Hawaii Standard"},
            new[] {"IDLE", "+1200", "Internation Date Line East"},
            new[] {"IDLW", "-1200", "Internation Date Line West"},
            new[] {"IST", "+0530", "Indian Standard"},
            new[] {"IT", "+0330", "Iran"},
            new[] {"JST", "+0900", "Japan Standard"},
            new[] {"JT", "+0700", "Java"},
            new[] {"MDT", "-0600", "(US) Mountain Daylight"},
            new[] {"MED", "+0200", "Middle European Daylight"},
            new[] {"MET", "+0100", "Middle European"},
            new[] {"MEST", "+0200", "Middle European Summer"},
            new[] {"MEWT", "+0100", "Middle European Winter"},
            new[] {"MST", "-0700", "(US) Mountain Standard"},
            new[] {"MOUNTAIN", "-0700", "(US) Mountain Standard"},
            new[] {"MT", "+0800", "Moluccas"},
            new[] {"NDT", "-0230", "Newfoundland Daylight"},
            new[] {"NFT", "-0330", "Newfoundland"},
            new[] {"NT", "-1100", "Nome"},
            new[] {"NST", "+0630", "North Sumatra"},
            new[] {"NZ", "+1100", "New Zealand "},
            new[] {"NZST", "+1200", "New Zealand Standard"},
            new[] {"NZDT", "+1300", "New Zealand Daylight "},
            new[] {"NZT", "+1200", "New Zealand"},
            new[] {"PDT", "-0700", "(US) Pacific Daylight"},
            new[] {"PST", "-0800", "(US) Pacific Standard"},
            new[] {"PACIFIC", "-0800", "(US) Pacific Standard"},
            new[] {"ROK", "+0900", "Republic of Korea"},
            new[] {"SAD", "+1000", "South Australia Daylight"},
            new[] {"SAST", "+0900", "South Australia Standard"},
            new[] {"SAT", "+0900", "South Australia Standard"},
            new[] {"SDT", "+1000", "South Australia Daylight"},
            new[] {"SST", "+0200", "Swedish Summer"},
            new[] {"SWT", "+0100", "Swedish Winter"},
            new[] {"USZ3", "+0400", "USSR Zone 3"},
            new[] {"USZ4", "+0500", "USSR Zone 4"},
            new[] {"USZ5", "+0600", "USSR Zone 5"},
            new[] {"USZ6", "+0700", "USSR Zone 6"},
            new[] {"UT", "-0000", "Universal Coordinated"},
            new[] {"UTC", "-0000", "Universal Coordinated"},
            new[] {"UZ10", "+1100", "USSR Zone 10"},
            new[] {"WAT", "-0100", "West Africa"},
            new[] {"WET", "-0000", "West European"},
            new[] {"WST", "+0800", "West Australian Standard"},
            new[] {"YDT", "-0800", "Yukon Daylight"},
            new[] {"YST", "-0900", "Yukon Standard"},
            new[] {"ZP4", "+0400", "USSR Zone 3"},
            new[] {"ZP5", "+0500", "USSR Zone 4"},
            new[] {"ZP6", "+0600", "USSR Zone 5"}
        };

        public static int ToInt32(this string value)
        {
            int result;
            var aBool = Int32.TryParse(value, out result);
            return aBool ? result : 0;
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime result;
            var aBool = DateTime.TryParse(value, out result);
            return aBool ? result : DateTime.MinValue;
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !(IsNullOrWhiteSpace(value));
        }

        public static bool DoesNotEqual(this string value, string valueToCompare)
        {
            return !(value.Equals(valueToCompare));
        }

        public static string ToSpaceSeperatedStringFromCamelCasedString(this string value)
        {
            return Regex.Replace(value, "(\\B[A-Z])", " $1");
        }

        public static DateTime ToDateTimeWithTimezone(this string value)
        {
            DateTime convertedDate = DateTime.MinValue;

            var zoneTable = new Hashtable(50);
            foreach (var timeZone in TimeZones)
            {
                zoneTable.Add(timeZone[0], timeZone[1]);
            }

            try
            {
                convertedDate = Convert.ToDateTime(value);
            }
            catch (FormatException)
            {
                // try finding a timezone in the date
                var tzStart = value.LastIndexOf(" ");

                if (tzStart != -1)
                {
                    var tzString = value.Substring(tzStart + 1); // +1 to avoid the space character
                    var tzValue = zoneTable[tzString] as string;

                    // its a timezone problem ...
                    if (tzValue != null)
                    {
                        // replace timezone name with actual hours (AEST = +1000)
                        var newDateStr = value.Replace(tzString, tzValue);

                        try
                        {
                            convertedDate = Convert.ToDateTime(newDateStr);
                        }
                        catch (FormatException)
                        {
                            // something else wrong, we don't know what to do
                        }
                    }
                }
            }

            return convertedDate;
        }
    }
}