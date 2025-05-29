using System;
using System.ComponentModel;

namespace FileManagerApp.Models;

public enum FileCategory
{
    File,
    Directory
}

public class FileItem : INotifyPropertyChanged
{
    private string _name;
    private string _path;

    public string Name
    {
        get => _name;
        private set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public FileCategory Category { get; }
    public long Size { get; }
    
    public string Path
    {
        get => _path;
        private set
        {
            _path = value;
            OnPropertyChanged(nameof(Path));
        }
    }

    public FileItem(string name, FileCategory category, long size, string path)
    {
        Name = name;
        Category = category;
        Size = category == FileCategory.Directory ? 0 : size;
        Path = path;
    }

    public FileItem Copy(string newPath) => new(Name, Category, Size, newPath);

    public void Move(string newPath)
    {
        if (string.IsNullOrWhiteSpace(newPath))
            throw new ArgumentException("Path cannot be empty");
        
        Path = newPath;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Name cannot be empty");
        
        Name = newName;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}