using System;
using System.Collections.Generic;

namespace AOC.Challenges {
    public class D7 {
        public abstract class DirectoryContent { }

        public class FileData : DirectoryContent {
            public string name;
            public int size;

            public FileData(string name, int size) {
                this.name = name;
                this.size = size;
            }
        }

        public class Directory : DirectoryContent {
            public string name;
            public List<DirectoryContent> contents;
            public int GetContentsSize() {
                int size = 0;
                foreach (DirectoryContent dc in contents) {
                    if (dc.GetType() == typeof(FileData)) {
                        FileData f = (FileData)dc;
                        size += f.size;
                    }
                    else {
                        Directory d = (Directory)dc;
                        size += d.GetContentsSize();
                    }
                }

                return size;
            }

            public Directory parentDirectory;

            public Directory(string name, Directory parentDirectory) {
                this.name = name;
                this.parentDirectory = parentDirectory;
                contents = new List<DirectoryContent>();
            }
        }


        public static void Run() {
            Directory startDirectory = GenerateDirectories();

            // Part 1
            int part1CombinedSize = GetSizeOfDirectoriesLessOrEqualToThreshold(startDirectory);
            Console.WriteLine("Total size of directories w/ size <= 100,000: " + part1CombinedSize);
            Console.WriteLine();

            // Part 2
            int totalSpaceUsed = startDirectory.GetContentsSize();
            int totalSpace = 70000000, minSpaceNeeded = 30000000;
            int thresholdSpace = minSpaceNeeded - (totalSpace - totalSpaceUsed);
            Directory smallestDirectoryGreaterOrEqualToThreshold =
                GetSmallestDirectoryGreaterOrEqualToThreshold(startDirectory, thresholdSpace);
            Console.WriteLine("Smallest directory w/ size >= " + thresholdSpace + ": " + smallestDirectoryGreaterOrEqualToThreshold.GetContentsSize());
            Console.WriteLine();
        }

        // Part 1
        public static int GetSizeOfDirectoriesLessOrEqualToThreshold(Directory directory, int threshold = 100000) {
            // Add current directory to size, if within threshold
            int size = directory.GetContentsSize(), combinedSize = 0;
            if (size <= threshold) {
                combinedSize += size;
            }

            // Add each subdirectory to size, if within threshold
            foreach (DirectoryContent dc in directory.contents) {
                if (dc.GetType() == typeof(Directory)) {
                    Directory d = (Directory)dc;
                    combinedSize += GetSizeOfDirectoriesLessOrEqualToThreshold(d);
                }
            }

            return combinedSize;
        }

        // Part 2
        public static Directory GetSmallestDirectoryGreaterOrEqualToThreshold(Directory directory, in int threshold, int minSize = int.MaxValue) {
            // Updates min directory with cur directory if needed
            Directory smallestDirectoryGreaterOrEqualToThreshold = null;
            int curDirectorySize = directory.GetContentsSize();
            if (curDirectorySize >= threshold && curDirectorySize < minSize) {
                minSize = curDirectorySize;
                smallestDirectoryGreaterOrEqualToThreshold = directory;
            }

            // Updates min directory with a subdirectory if needed
            foreach (DirectoryContent dc in directory.contents) {
                if (dc.GetType() == typeof(Directory)) {
                    Directory d = (Directory)dc;
                    Directory sd = GetSmallestDirectoryGreaterOrEqualToThreshold(d, threshold, minSize);
                    if (sd == null) {
                        continue;
                    }

                    int sdSize = sd.GetContentsSize();
                    if (sdSize < curDirectorySize) {
                        minSize = sdSize;
                        smallestDirectoryGreaterOrEqualToThreshold = sd;
                    }
                }
            }

            return smallestDirectoryGreaterOrEqualToThreshold;
        }


        #region Helpers
        private static Directory GenerateDirectories() {
            Directory startDirectory = null, curDirectory = null;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D7\\D7_Input.txt");
            foreach (string input in inputs) {
                string[] tokens = input.Split(' ');
                if (tokens[0].Equals("$") && tokens[1].Equals("cd")) {
                    if (tokens[2].Equals("..")) { // Goto parent directory
                        curDirectory = curDirectory.parentDirectory;
                    }
                    else if (tokens[2].Equals("/")) { // Create starting directory
                        startDirectory = curDirectory = new Directory("/", null);
                    }
                    else { // Find a goto a child directory
                        string gotoDirectoryName = tokens[2];
                        foreach (DirectoryContent dc in curDirectory.contents) {
                            if (dc.GetType() != typeof(Directory)) {
                                continue;
                            }

                            Directory d = (Directory)dc;
                            if (d.name == gotoDirectoryName) {
                                curDirectory = d;
                                break;
                            }
                        }
                    }
                }
                else if (tokens[0].Equals("$") && tokens[1].Equals("ls")) { } // ls lines skipped
                else if (tokens[0].Equals("dir")) { // Create a subdirectory in the current directory
                    string directoryName = tokens[1];

                    Directory newDirectory = new Directory(directoryName, curDirectory);
                    curDirectory.contents.Add(newDirectory);
                }
                else { // Create a file in the current directory
                    int fileSize = int.Parse(tokens[0]);
                    string fileName = tokens[1];

                    FileData newFile = new FileData(fileName, fileSize);
                    curDirectory.contents.Add(newFile);
                }
            }

            return startDirectory;
        }
        #endregion
    }
}
