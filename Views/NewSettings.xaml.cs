using confighandler;
using log4net;
using log4net.Config;
using System.Diagnostics;

namespace BambuLab_FailureDetection.Views;

public partial class NewSettings : ContentPage
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	private static readonly string SettingsKey = @"SOFTWARE\connor33341\BambuLabFailureDetector";
	readonly string[][] ConfigList = [
		["Printer Name","Enter a name for your printer, this will show up on alerts (if enabled).","Bambulab Printer",""],
		["Printer SN","Enter your printer SN, found in settings on your printer","",""],
		["Printer Access Code","Found in settings on your printer","",""],
		["Printer URL","IP Address of your printer 10.0.0.1 by default","10.0.0.1",""],
		["Printer Port","Port of your printer. Defaults to 8080","8080",""]
		];
	List<string> Values = new List<string>();
	List<Entry> EntryList = new List<Entry>();
	public NewSettings()
	{
		InitializeComponent();
		SetupCards();
	}

	public void SetupCards()
	{
        if (log.IsInfoEnabled) log.Info("[Views\\NewSettings.xmal.cs]: Setting up cards");
        for (int i = 0; i < ConfigList.Length; i++) { 
			string[] ConfigData = ConfigList[i];
			string Title = ConfigData[0];
			string Description = ConfigData[1];
			string DefaultValue = ConfigData[2];

			Label TitleLabel = new Label
			{
				Text = Title,
				FontAttributes = FontAttributes.Bold,
				FontSize = 14,
				HorizontalTextAlignment = TextAlignment.Center,
			};

			BoxView Line = new BoxView 
			{ 
				Color = Colors.BlueViolet,
				HeightRequest = 2,
				HorizontalOptions = LayoutOptions.Fill
			};

			Label DescriptionLabel = new Label
			{
				Text= Description,
			};

			Entry TextInput = new Entry 
			{ 
				//Text = DefaultValue,
				Placeholder = DefaultValue,
			};

			TextInput.TextChanged += (sender, e) =>
			{
				//Console.WriteLine(i);
				//ConfigList[i][3] = e.NewTextValue;
				//Values.Add(e.NewTextValue);
			};

			BoxView Line2 = new BoxView
			{
				Color = Colors.Transparent,
				HeightRequest = 5,
				HorizontalOptions = LayoutOptions.Center,
			};

			StackLayout Layout = new StackLayout 
			{ 
				Padding = 5,
			};

			Layout.Add( TitleLabel );
			Layout.Add( Line );
			Layout.Add( DescriptionLabel );
			Layout.Add( Line2 );
			Layout.Add( TextInput );

			Frame Card = new Frame 
			{ 
				
			};

			Card.Content = Layout;
			
			SettingsFrame.Add(Card);
			EntryList.Add(TextInput);

		}
	}

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
		ConfigHandler configHandler = new ConfigHandler();
		configHandler.SubKey = SettingsKey;
		configHandler.ValidSubKey = true;

		for (int i = 0; i < EntryList.Count; i++)
		{
			Entry TextInput = EntryList[i];
			string Text = TextInput.Text;
			Values.Add(Text);
		}
		string[][] OutputData = [];
		for (int i = 0; i < ConfigList.Length-1; i++)
		{
			string[] ConfigData = ConfigList[i];
			string ValueName = ConfigData[0];
			string DefaultValue = ConfigData[2];
			string Data = Values[i];
			Debug.WriteLine("Value: " + ValueName + " Data: " + Data);
			if (Data == "")
			{
				Data = DefaultValue;
			}
			//OutputData[i] = [ValueName, Data];
			try
			{
                configHandler.SetKey(ValueName, Data);
            }
			catch(Exception Ex)
			{
				Debug.WriteLine(Ex);
			}
        }
        await Navigation.PushAsync(new MainPage());
    }
}