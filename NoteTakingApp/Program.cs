using System;
using System.IO;
using System.Collections.Generic;

namespace NoteTakingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!AuthenticateUser())
            {
                Console.WriteLine("Access denied.");
                return;
            }
            string filePath = "notes.txt";
            List<string> notes = new List<string>();
            if (File.Exists(filePath))
            {
                notes.AddRange(File.ReadAllLines(filePath));
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Note-Taking App");
                Console.WriteLine("1. View Notes");
                Console.WriteLine("2. Add Note");
                Console.WriteLine("3. Delete Note");
                Console.WriteLine("4. Exit");
                Console.WriteLine("5. Search Notes");
                Console.WriteLine("6. Edit Note");
                Console.WriteLine("7. Backup Notes");
                Console.WriteLine("8. Restore Notes");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewNotes(notes);
                        break;
                    case "2":
                        AddNoteWithCategory(notes, filePath);
                        break;
                    case "3":
                        DeleteNote(notes, filePath);
                        break;
                    case "4":
                        return;
                    case "5":
                        SearchNotes(notes);
                        break;
                    case "6":
                        EditNote(notes, filePath);
                        break;
                    case "7":
                        BackupNotes(filePath);
                        break;
                    case "8":
                        RestoreNotes(notes, filePath);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static void ViewNotes(List<string> notes)
        {
            Console.Clear();
            if (notes.Count == 0)
            {
                Console.WriteLine("No notes available.");
            }
            else
            {
                Console.WriteLine("Notes:");
                for (int i = 0; i < notes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {notes[i]}");
                }
            }
        }

        static void AddNoteWithCategory(List<string> notes, string filePath)
        {
            Console.Clear();
            Console.Write("Enter your note: ");
            string newNote = Console.ReadLine();
            Console.Write("Enter category: ");
            string category = Console.ReadLine();
            string categorizedNote = $"{DateTime.Now} [{category}]: {newNote}";

            notes.Add(categorizedNote);
            File.AppendAllLines(filePath, new[] { categorizedNote });
            Console.WriteLine("Note with category added successfully.");
        }
        static void EditNote(List<string> notes, string filePath)
        {
            Console.Clear();
            ViewNotes(notes);
            Console.Write("\nEnter the number of the note to edit: ");
            if (int.TryParse(Console.ReadLine(), out int noteIndex) && noteIndex > 0 && noteIndex <= notes.Count)
            {
                Console.Write("Enter the new content for the note: ");
                string newContent = Console.ReadLine();
                notes[noteIndex - 1] = $"{DateTime.Now} (Edited): {newContent}";

                File.WriteAllLines(filePath, notes);
                Console.WriteLine("Note edited successfully.");
            }
            else
            {
                Console.WriteLine("Invalid note number.");
            }
        }

        static void DeleteNote(List<string> notes, string filePath)
        {
            Console.Clear();
            ViewNotes(notes);
            Console.Write("\nEnter the number of the note to delete: ");
            if (int.TryParse(Console.ReadLine(), out int noteIndex) && noteIndex > 0 && noteIndex <= notes.Count)
            {
                notes.RemoveAt(noteIndex - 1);
                File.WriteAllLines(filePath, notes);
                Console.WriteLine("Note deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid note number.");
            }
        }
        static void SearchNotes(List<string> notes)
        {
            Console.Clear();
            Console.Write("Enter keyword to search: ");
            string keyword = Console.ReadLine()?.ToLower();

            var foundNotes = notes.FindAll(note => note.ToLower().Contains(keyword));

            if (foundNotes.Count == 0)
            {
                Console.WriteLine("No notes found with that keyword.");
            }
            else
            {
                Console.WriteLine("Search Results:");
                foreach (var note in foundNotes)
                {
                    Console.WriteLine($"- {note}");
                }
            }
        }

        static bool AuthenticateUser()
        {
            Console.Write("Enter password to access the app: ");
            string inputPassword = Console.ReadLine();
            const string correctPassword = "your_password_here"; // Replaceeeeeeeeeeeeeee
            return inputPassword == correctPassword;
        }

        static void BackupNotes(string filePath)
        {
            string backupPath = "notes_backup.txt";
            if (File.Exists(filePath))
            {
                File.Copy(filePath, backupPath, true);
                Console.WriteLine("Notes backed up successfully.");
            }
            else
            {
                Console.WriteLine("No notes file found to back up.");
            }
        }
        static void RestoreNotes(List<string> notes, string filePath)
        {
            string backupPath = "notes_backup.txt";
            // Check if the backup file exists 
            if (File.Exists(backupPath))
            {
                notes.Clear(); // Clear current notes in memory
                notes.AddRange(File.ReadAllLines(backupPath)); 
                File.WriteAllLines(filePath, notes);
                Console.WriteLine("Notes restored from backup.");
            }
            else
            {
                Console.WriteLine("No backup file found.");
            }
        }



    }
}
