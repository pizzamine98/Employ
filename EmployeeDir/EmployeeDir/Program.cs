using System;

namespace EmployeeDir
{
    
        class Program
        {
            static void Main(string[] args)
            {
                Manager man = new Manager();
                man.SetupStuff();
            }
        }
        class Manager
        {
            public void SetupStuff()
            {
                bored = false;
                emps = new Employees();
                emps.Setup(10000);
            while (!bored)
            {
                Console.WriteLine("*var* means a variable.");
                Console.WriteLine("Enter \"set *filepath*\" to set your employee file , or \"makedir *directorypath*\" to make folder and file.");
                command = Console.ReadLine();
                things = command.Split(" ");

                if (things[0].Equals("set"))
                {
                    path = @"" + things[1];
                    emps.SetPath(path);
                    emps.LoadFile();
                    bored = true;
                }
                else if (things[0].Equals("makedir"))
                {
                    path = @"" + things[1];
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    path = path + "\\employees.dat";
                    System.IO.File.WriteAllText(path, "");
                    Console.WriteLine("New filepath: " + path);
                    emps.SetPath(path);
                    emps.LoadFile();
                    bored = true;
                }
                
            }
                bored = false;
                Console.WriteLine("Enter \"help\" to get a list of commands or enter command below (command is the text in parentheses and *var* means a variable:");
                Console.WriteLine("Note: For any name variable, if you just enter the last name, it will select the first name with that last name.");
            while (!bored)
                {
                    
                    command = Console.ReadLine();
                    if (command.Equals("help"))
                    {
                        Console.WriteLine("~\"printout\" to print all employee information");
                        Console.WriteLine("~\"exit\" to exit program");
                        Console.WriteLine("~\"modify,*name*\" to modify an employee");
                        Console.WriteLine("~\"add,*name*,*jobtitle*\" to add an employee");
                        Console.WriteLine("~\"delete,*name*\" to delete an employee");

                    }
                    else if (command.Equals("printout"))
                    {
                        emps.PrintOut();
                    }
                    else if (command.Equals("exit"))
                    {
                        bored = true;
                        Console.WriteLine("Have a nice day!");
                    }
                    else if (command.StartsWith("modify"))
                    {
                        things = command.Split(",");
                        name = things[1];

                        Console.WriteLine("Enter \"*whichtomod(name/jobtitle)*,*newvalue*\" to modify");
                        Console.WriteLine("Ex: \"name,Smith John\"");
                        Console.WriteLine("Ex: \"jobtitle,Junior Developer\"");
                        lone = Console.ReadLine();
                        things2 = lone.Split(",");
                        emps.Modify(name, things2[0], things2[1]);
                        emps.WriteFile2();
                        emps.LoadFile();
                        Console.WriteLine("Employee Modified");
                    }
                    else if (command.StartsWith("add"))
                    {
                        emps.FindEmptySlot();
                        things2 = command.Split(",");
                        emps.AddInSlot(things2[1], things2[2]);
                        emps.WriteFile2();
                        emps.LoadFile();
                        Console.WriteLine("Employee Added");
                    }
                    else if (command.StartsWith("delete"))
                    {
                        things = command.Split(",");
                        emps.Delete(things[1]);
                        emps.WriteFile2();
                        emps.LoadFile();
                        Console.WriteLine("Employee Deleted");
                    }
                    
                }
            }
            private string command, path, name, tomod, lone;

            private string[] things, things2;
            private Employees emps;
            private string dir;
            private bool bored;
        }
        class Employees
        {
            public void SetPath(string pathin)
            {
                path = pathin;
            }
            public void Setup(int maxempin)
            {
                maxemp = maxempin;
                real = new bool[maxemp];
                info = new string[maxemp, 2];
                for (int iu = 0; iu < maxemp; iu++)
                {
                    real[iu] = false;
                    info[iu, 0] = "";
                    info[iu, 1] = "";
                }

            }
            public void FindIndex(string namein)
            {
                ind2 = -1;
                for (int iu = 0; iu < maxemp; iu++)
                {
                    if (info[iu, 0].StartsWith(namein) && real[iu])
                    {
                        ind2 = iu;
                        iu = maxemp;
                    }
                }

            }
            public void FindNReals()
            {
                nres = 0;
                for (int iu = 0; iu < maxemp; iu++)
                {
                    if (real[iu])
                    {
                        nres++;
                    }
                }
            }
            private int nres;
            public void Modify(string orignamein, string tomodin, string newvalin)
            {
                FindIndex(orignamein);
                if (tomodin.Equals("name"))
                {
                    ind3 = 0;
                }
                else if (tomodin.Equals("jobtitle"))
                {
                    ind3 = 1;
                }
                info[ind2, ind3] = newvalin;

            }
            public void WriteFile2()
            {
                FindNReals();
                lines = new string[nres];
                c10 = 0;
                for (int ius = 0; ius < maxemp; ius++)
                {
                    if (real[ius])
                    {
                        lines[c10] = info[ius, 0] + "," + info[ius, 1];
                        c10++;
                    }
                }
                WriteFile();
            }
            private int ind2, ind3, c10;
            public void FindEmptySlot()
            {
                slot0 = -1;
                for (int iu = 0; iu < maxemp; iu++)
                {
                    if (!real[iu])
                    {
                        slot0 = iu;
                        iu = maxemp;
                    }
                }
            }
            public void AddInSlot(string namein, string jobtitlein)
            {
                info[slot0, 0] = namein;
                info[slot0, 1] = jobtitlein;
                real[slot0] = true;

            }
            public void Delete(string namein)
            {
                FindIndex(namein);
                real[ind2] = false;

            }
            public void Reset()
            {
                real = new bool[maxemp];
                info = new string[maxemp, 2];
                for (int iu = 0; iu < maxemp; iu++)
                {
                    real[iu] = false;
                    info[iu, 0] = "";
                    info[iu, 1] = "";
                }
            }
            public void LoadLines(string[] linesin)
            {
                /*
                 * name,jobtitle
                 * 
                 * 
                 */
                lines = linesin;
                Reset();
                nl = lines.Length;

                for (int iuu = 0; iuu < nl; iuu++)
                {

                    things = lines[iuu].Split(",");
                    real[iuu] = true;
                    info[iuu, 0] = things[0];
                    info[iuu, 1] = things[1];
                }
            }
            public void MakeLines()
            {
                nreal = 0;
                for (int iuu = 0; iuu < maxemp; iuu++)
                {
                    if (real[iuu])
                    {
                        nreal++;
                    }
                }
                lines = new string[nreal];
                cl = 0;
                for (int iuu = 0; iuu < maxemp; iuu++)
                {
                    if (real[iuu])
                    {
                        lines[cl] = info[iuu, 0] + "," + info[iuu, 1];
                    }
                }
                LoadLines(lines);
            }
            public void PrintOut()
            {
                Console.WriteLine("Employee Printout: number of employees = " + nl);
                Console.WriteLine("lastname firstname,jobtitle");
                for (int iuu = 0; iuu < maxemp; iuu++)
                {
                    if (real[iuu])
                    {
                        Console.WriteLine(info[iuu, 0] + "," + info[iuu, 1]);
                    }
                }
            }
            public void PrintOutNames()
            {
                Console.WriteLine("Employee Names Printout: number of employees = " + nl);
                for (int iuu = 0; iuu < maxemp; iuu++)
                {
                    if (real[iuu])
                    {
                        Console.WriteLine(info[iuu, 0]);
                    }
                }
            }
            public void WriteFile()
            {
                System.IO.File.WriteAllLines(path, lines);
            }
            public void LoadFile()
            {
                lines = System.IO.File.ReadAllLines(path);
                LoadLines(lines);
            }
            private int nreal, cl;
            private string[,] info;
            private string path;
            private bool[] real;
            private string[] lines, things;
            private int maxemp, slot0, nl;
        }
    }

