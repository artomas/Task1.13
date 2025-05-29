using System;
using System.Windows.Input;
using FileManagerApp.Models;

namespace FileManagerApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private FileItem _currentFile;
    public FileItem CurrentFile
    {
        get => _currentFile;
        set
        {
            _currentFile = value;
            OnPropertyChanged(nameof(CurrentFile));
            OnPropertyChanged(nameof(CurrentFile.Name));
            OnPropertyChanged(nameof(CurrentFile.Path));
        }
    }

    public ICommand CopyCommand { get; }
    public ICommand MoveCommand { get; }
    public ICommand RenameCommand { get; }

    public MainWindowViewModel()
    {
        CurrentFile = new FileItem("document.txt", FileCategory.File, 1024, "C:\\Temp\\original.txt");

        CopyCommand = new RelayCommand(_ =>
        {
            var newPath = $"C:\\Temp\\copy_of_{CurrentFile.Name}";
            CurrentFile = CurrentFile.Copy(newPath);
        });

        MoveCommand = new RelayCommand(_ =>
        {
            var newPath = $"C:\\Temp\\moved\\{CurrentFile.Name}";
            CurrentFile.Move(newPath);
            CurrentFile = CurrentFile;
        });

        RenameCommand = new RelayCommand(_ =>
        {
            CurrentFile.Rename($"renamed_{CurrentFile.Name}");
            CurrentFile = CurrentFile;
        });
    }
}