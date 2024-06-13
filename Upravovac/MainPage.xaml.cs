using Upravovac.Model;

namespace Upravovac
{
    public partial class MainPage : ContentPage
    {
        string path;
        public string content;
        public string[] splited;
        public string word;
        public string firstLetter;
        public Upravovator u;


        public MainPage()
        {
            InitializeComponent();
            u = new Upravovator();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            FileResult result = await OpenFileDialog();
            if (result != null)
            {
                path = result.FullPath;
                OpenedFile.Text = $"Soubor: {result.FileName}";
                Read();
            }
        }

        public void Read()
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    content = sr.ReadToEnd();
                    EIn.Text = content;
                }
            }
        }

        private async Task<FileResult> OpenFileDialog()
        {
            FilePickerFileType filetype = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            {DevicePlatform.WinUI,new[]{".txt",".csv"} }
        });

            PickOptions options = new PickOptions()
            {
                PickerTitle = "Vyber textový soubor",
                FileTypes = filetype
            };

            return await FilePicker.Default.PickAsync(options);
        }

        private void EIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveBtn.BackgroundColor = Colors.Red;
            SaveBtn.Text = "Uložit";
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                u.Upravuj(content);
                EOut.Text = u._content;
                
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(EIn.Text);
                }
                SaveBtn.BackgroundColor = Colors.Green;
                SaveBtn.Text = "Uloženo";
            }
            catch
            {
                DisplayAlert("POZOR!", "Musíš nejprve vybrat soubor", "OK");
            }
        }
    }
}