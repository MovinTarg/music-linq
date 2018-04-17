using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================
            Console.WriteLine();
            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Console.WriteLine("There is only one artist in this collection from Mount Vernon, what is their name and age?");
            Artist answer = Artists.Where( str => str.Hometown == "Mount Vernon").Single();
            Console.WriteLine($"Name: {answer.ArtistName}");
            Console.WriteLine($"Age: {answer.Age}");
            Console.WriteLine();
            //Who is the youngest artist in our collection of artists?
            Console.WriteLine("Who is the youngest artist in our collection of artists?");
            Artist youngest = Artists[0];
            for (int i = 0; i < Artists.Count; i++){
                if (Artists[i].Age < youngest.Age){
                    youngest = Artists[i];
                }
            }
            Console.WriteLine($"Name: {youngest.ArtistName}");
            Console.WriteLine($"Age: {youngest.Age}");
            Console.WriteLine();
            //Display all artists with 'William' somewhere in their real name
            Console.WriteLine("Display all artists with 'William' somewhere in their real name");
            List<Artist> williams = new List<Artist>(Artists.Where(str => str.RealName.Contains("William")));
            for (int i = 0; i < williams.Count; i++){
                Console.WriteLine($"Name: {williams[i].ArtistName}");
                Console.WriteLine($"Age: {williams[i].RealName}");
                Console.WriteLine("----------");
            }
            Console.WriteLine();
            //Display all groups with names less than 8 characters in length.
            Console.WriteLine("Display all groups with names less than 8 characters in length.");
            List<Group> groupNames = new List<Group>(Groups.Where(str => str.GroupName.Length < 8));
            for (int i = 0; i < groupNames.Count; i++){
                Console.WriteLine($"Name: {groupNames[i].GroupName}");
                Console.WriteLine("----------");
            }
            Console.WriteLine();
            //Display the 3 oldest artist from Atlanta
            Console.WriteLine("Display the 3 oldest artist from Atlanta");
            List<Artist> oldest = new List<Artist>(Artists.Where(str => str.Hometown == "Atlanta").OrderByDescending(str => str.Age).Take(3));
            for (int i = 0; i < oldest.Count; i++){
                Console.WriteLine($"Name: {oldest[i].ArtistName}");
                Console.WriteLine($"Age: {oldest[i].Age}");
                Console.WriteLine("----------");
            }
            Console.WriteLine();
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            Console.WriteLine("Display the Group Name of all groups that have members that are not from New York City.");
            List<Group> notNycGroups = new List<Group>(Groups.GroupJoin(Artists, group => group.Id, artist => artist.GroupId,
                            (group, artists) => { group.Members = artists.ToList(); return group;}));
            for (int i = 0; i < notNycGroups.Count; i++){
                for (int j = 0; j < notNycGroups[i].Members.Count; j++){
                    if (notNycGroups[i].Members[j].Hometown == "New York City"){
                        notNycGroups.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i < notNycGroups.Count; i++){
                Console.WriteLine($"Name: {notNycGroups[i].GroupName}");
                Console.WriteLine("----------");
            }
            Console.WriteLine();
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            Console.WriteLine("Display the artist names of all members of the group Wu-Tang Clan.");
            Group clanWu = Groups.Where(str => str.GroupName == "Wu-Tang Clan").GroupJoin(Artists, group => group.Id, artist => artist.GroupId,
                                        (group, artists) => { group.Members = artists.ToList(); return group;}).Single();;
            for (int i = 0; i < clanWu.Members.Count; i++){
                Console.WriteLine($"Name: {clanWu.Members[i].ArtistName}");
                Console.WriteLine("----------");
            }
            Console.WriteLine();
        }
    }
}
