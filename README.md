RateControl
======= 
This is a .NET rating user control built using C#/XAML. The user control targets Windows 10 apps but can be used across all .NET applications.

![stylized version](https://github.com/SezginEge/RateControl/blob/master/img/ratecontrol-demo.gif)

## Installation

There is no nuget package :/ If you want to use rate control on your project, you should add rate control project to your existing project as reference.

## Usage

### Configurations:

| Property			| Description| Default Value |
| ------------------|------------|---------------|
| IsOpen | sets a value that indicates whether the rate control is visible. | False |
| Title	| text of title | RATE US! |
| TitleForegroundColor	| color of title | Black |
| TitleFontSize				| font size of title | 18 |
| StoreMessage	| text to be displayed to the user after giving points |Do you want to give us some feedback? |
| StoreMessageForegroundColor	| color of store message | Black |
| StoreMessageFontSize				| font size of store message | 16 |
| Height | height of user control | 120 |
| Widht | width of user control | 420 |
| Background | background color | White |
| RateItemCount | number of rating item	| - |
| RateItemIco | icon of rate item that uses vector data | Star |
| RatingValue | ignore this | 0 |
| ApiUrl | api url will be sent the results | empty |
| ApplicationName | name of application | empty |
| OkContent | content of okay button | Go to store! |
| CancelContent | content of cancel button | Thanks, later :/ |
| OkCommand| function that will be invoked if clicks okay button| - |
| CancelCommand | function that will be invoked if clicks cancel button | - |

We can instantiate a rate control like so:

XAML:

```xml
   <RateControl:RateUserControl x:Name="rate"
                                     ApiUrl="http://localhost:60214/api/Statistic/New"
                                     ApplicationName="RateControlDemo"
                                     RateItemCount="5"
                                     RateItemIco="F1 M 145.637,174.227L 127.619,110.39L 180.809,70.7577L 114.528,68.1664L 93.2725,5.33333L 70.3262,67.569L 4,68.3681L 56.0988,109.423L 36.3629,172.75L 91.508,135.888L 145.637,174.227 Z" HorizontalAlignment="Stretch" VerticalAlignment="Stretch"/>

  
```

C# (code-behind):

```c#
		public MainPage()
        {
            this.InitializeComponent();
            rate.CancelCommand = new RelayCommand(CancelClicked);
            rate.OkCommand = new RelayCommand(OkClicked);
        }

        public void CancelClicked(object x)
        {
            rate.IsOpen = false;
        }

        public async void OkClicked(object x)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp"));
            rate.IsOpen = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            rate.IsOpen = !rate.IsOpen;
        }
```

Take a look at the [demo project](https://github.com/SezginEge/RateControl/tree/master/RateControlDemo) for a closer look.


## License

*  Licensed under [The MIT License (MIT)](https://github.com/SezginEge/RateControl/blob/master/LICENSE).





