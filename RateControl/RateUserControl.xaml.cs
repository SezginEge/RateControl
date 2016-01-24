using RateControl.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace RateControl
{
    public sealed partial class RateUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(caller));
            }
        }

        #region Ctor
        public RateUserControl()
        {
            this.InitializeComponent();
            this.DataContext = this;

            var bounds = Window.Current.Bounds;
            this.RootPanel.Width = bounds.Width;
            this.RootPanel.Height = bounds.Height;

            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.RootPanel.Width = e.Size.Width;
            this.RootPanel.Height = e.Size.Height;
        }
        #endregion

        #region Title
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(RateUserControl), new PropertyMetadata("RATE US!", null));
        #endregion

        #region TitleFontSize
        public double TitleFontSize
        {
            get
            {
                return (double)GetValue(TitleFontSizeProperty);
            }
            set
            {
                SetValue(TitleFontSizeProperty, value);
            }
        }

        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(RateUserControl), new PropertyMetadata(18.0, null));

        #endregion

        #region TitleForeground
        public Brush TitleForeground
        {
            get
            {
                return (Brush)GetValue(TitleForegroundProperty);
            }
            set
            {
                SetValue(TitleForegroundProperty, value);
            }
        }

        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(RateUserControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), null));

        #endregion

        #region StoreMessage
        public string StoreMessage
        {
            get
            {
                return (string)GetValue(StoreMessageProperty);
            }
            set
            {
                SetValue(StoreMessageProperty, value);
            }
        }

        public static readonly DependencyProperty StoreMessageProperty =
            DependencyProperty.Register("StoreMessage", typeof(string), typeof(RateUserControl), new PropertyMetadata("Do you want to give us some feedback?", null));
        #endregion

        #region StoreMessageFontSize
        public double StoreMessageFontSize
        {
            get
            {
                return (double)GetValue(StoreMessageFontSizeProperty);
            }
            set
            {
                SetValue(StoreMessageFontSizeProperty, value);
            }
        }

        public static readonly DependencyProperty StoreMessageFontSizeProperty =
            DependencyProperty.Register("StoreMessageFontSize", typeof(double), typeof(RateUserControl), new PropertyMetadata(16.0, null));

        #endregion

        #region StoreMessageForeground
        public Brush StoreMessageForeground
        {
            get
            {
                return (Brush)GetValue(StoreMessageForegroundProperty);
            }
            set
            {
                SetValue(StoreMessageForegroundProperty, value);
            }
        }

        public static readonly DependencyProperty StoreMessageForegroundProperty =
            DependencyProperty.Register("StoreMessageForeground", typeof(Brush), typeof(RateUserControl), new PropertyMetadata(new SolidColorBrush(Colors.Black), null));

        #endregion

        #region CancelContent
        public string CancelContent
        {
            get
            {
                return (string)GetValue(CancelContentProperty);
            }
            set
            {
                SetValue(CancelContentProperty, value);
            }
        }

        public static readonly DependencyProperty CancelContentProperty =
            DependencyProperty.Register("CancelContent", typeof(string), typeof(RateUserControl), new PropertyMetadata("Thanks, later :/", null));
        #endregion

        #region OkContent
        public string OkContent
        {
            get
            {
                return (string)GetValue(OkContentProperty);
            }
            set
            {
                SetValue(OkContentProperty, value);
            }
        }

        public static readonly DependencyProperty OkContentProperty =
            DependencyProperty.Register("OkContent", typeof(string), typeof(RateUserControl), new PropertyMetadata("Go to Store!", null));
        #endregion

        #region Background
        public new Brush Background
        {
            get
            {
                return (Brush)GetValue(BackgroundProperty);
            }
            set
            {
                SetValue(BackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(RateUserControl), new PropertyMetadata(new SolidColorBrush(Colors.White), null));

        #endregion

        #region Height
        public new double Height
        {
            get
            {
                return (double)GetValue(HeightProperty);
            }
            set
            {
                SetValue(HeightProperty, value);
            }
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(RateUserControl), new PropertyMetadata(120.0, null));

        #endregion

        #region Width
        public new double Width
        {
            get
            {
                return (double)GetValue(WidthProperty);
            }
            set
            {
                SetValue(WidthProperty, value);
            }
        }

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(RateUserControl), new PropertyMetadata(420.0, null));

        #endregion

        #region Count
        public int RateItemCount
        {
            get
            {
                return (int)GetValue(RateItemCountProperty);
            }
            set
            {
                SetValue(RateItemCountProperty, value);
                SetupRatingItems();
            }
        }

        public static readonly DependencyProperty RateItemCountProperty =
            DependencyProperty.Register("RateItemCount", typeof(int), typeof(RateUserControl), new PropertyMetadata(5, null));

        #endregion

        #region APIUrl
        public string ApiUrl
        {
            get
            {
                return (string)GetValue(ApiUrlProperty);
            }
            set
            {
                SetValue(ApiUrlProperty, value);
            }
        }

        public static readonly DependencyProperty ApiUrlProperty =
            DependencyProperty.Register("ApiUrl", typeof(string), typeof(RateUserControl), new PropertyMetadata(string.Empty, null));

        #endregion

        #region ApplicationName
        public string ApplicationName
        {
            get
            {
                return (string)GetValue(ApplicationNameProperty);
            }
            set
            {
                SetValue(ApplicationNameProperty, value);
            }
        }

        public static readonly DependencyProperty ApplicationNameProperty =
            DependencyProperty.Register("ApplicationNameUrl", typeof(string), typeof(RateUserControl), new PropertyMetadata(string.Empty, null));

        #endregion

        #region RateItemIco
        public string RateItemIco
        {
            get
            {
                return (string)GetValue(RateItemIcoProperty);
            }
            set
            {
                SetValue(RateItemIcoProperty, value);
                SetupRatingItems();
            }
        }

        public static readonly DependencyProperty RateItemIcoProperty =
            DependencyProperty.Register("RateItemIco", typeof(Geometry), typeof(RateUserControl), new PropertyMetadata("F1 M 58.0746,26.9242C 58.0746,34.5584 53.6059,43.5302 44.6685,53.8394L 38.0631,61.1958L 31.1903,53.8394C 22.2597,43.5302 17.7944,34.5584 17.7944,26.9242C 17.7944,23.9817 18.813,21.4335 20.8502,19.2798C 22.6747,17.4141 25.2709,16.4812 28.6387,16.4812C 31.6636,16.4812 34.0265,17.3283 35.7276,19.0225C 36.8731,20.2092 37.6242,21.4335 37.9808,22.6956C 38.2758,21.4335 38.9308,20.2983 39.946,19.29C 41.9625,17.4175 44.3907,16.4812 47.2303,16.4812C 50.5913,16.4812 53.1875,17.4141 55.0189,19.2798C 57.056,21.4404 58.0746,23.9885 58.0746,26.9242 Z ", null));


        #endregion

        #region RatingValue
        public static readonly DependencyProperty RatingValueProperty =
           DependencyProperty.Register("RatingValue", typeof(int), typeof(RateUserControl),
           new PropertyMetadata(0, new PropertyChangedCallback(RatingValueChanged)));


        private int maxValue = 5;
        public int RatingValue
        {
            get { return (int)GetValue(RatingValueProperty); }
            set
            {
                if (value < 0)
                {
                    SetValue(RatingValueProperty, 0);
                }
                else if (value > maxValue)
                {
                    SetValue(RatingValueProperty, maxValue);
                }
                else
                {
                    SetValue(RatingValueProperty, value);
                }
            }
        }

        private static void RatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RateUserControl parent = sender as RateUserControl;
            if (parent != null)
                parent.OnRatingValueChanged(sender, e);
        }

        private async void OnRatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RateUserControl parent = sender as RateUserControl;
            int ratingValue = (int)e.NewValue;

            for (int i = 0; i < ratingValue; i++)
            {
                RatingItems.ElementAt(i).IsChecked = true;
            }

            for (int i = ratingValue; i < RatingItems.Count; i++)
            {
                RatingItems.ElementAt(i).IsChecked = false;
            }

            if (ratingValue > 0)
            {
                await Task.Delay(500);
                RatingItemsContent.Visibility = Visibility.Collapsed;
                AfterRatingContent.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region RatingItems
        private ObservableCollection<RateItem> ratingItems;
        public ObservableCollection<RateItem> RatingItems
        {
            get
            {
                return ratingItems;
            }
            set { ratingItems = value; OnPropertyChanged<ObservableCollection<RateItem>>("RatingItems"); }
        }

        public void SetupRatingItems()
        {
            if (RatingItems == null)
            {
                RatingItems = new ObservableCollection<RateItem>();
            }
            RatingItems.Clear();
            for (int i = 0; i < RateItemCount; i++)
            {
                RatingItems.Add(new RateItem()
                {
                    Tag = (i + 1),
                    RatingItemClickCommand = new RelayCommand(RatingItemClickCommand)
                });
            }
        }

        private void RatingItemClickCommand(object sender)
        {
            ToggleButton button = sender as ToggleButton;
            int newRating = int.Parse(button.Tag.ToString());

            if ((bool)button.IsChecked || newRating < RatingValue)
            {
                RatingValue = newRating;
            }
            else
            {
                RatingValue = newRating - 1;
            }
            
            Task.Factory.StartNew(
                async () => {
                    await SendRatingResult();
                }, 
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task SendRatingResult()
        {
            if (!string.IsNullOrWhiteSpace(ApiUrl))
            {
                var rateResult = new RateResult()
                {
                    ApplicationName =  ApplicationName,
                    NumberOfRateItem = RateItemCount,
                    Result = RatingValue
                };
                using (HttpClient client = new HttpClient())
                {
                    await client.PostAsJsonAsync(ApiUrl, rateResult);
                }
            }
        }

        #endregion

        #region IsOpen
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                if (value)
                {
                    ShowPopup.Begin();
                }
                else
                {
                    HidePopup.Begin();
                    RatingItemsContent.Visibility = Visibility.Visible;
                    AfterRatingContent.Visibility = Visibility.Collapsed;
                    RatingValue = 0;
                }
                SetValue(IsOpenProperty, value);
            }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(RateUserControl), new PropertyMetadata(false, null));

        #endregion

        #region OkCommand
        public ICommand OkCommand
        {
            get
            {
                return (ICommand)GetValue(OkCommandProperty);
            }
            set
            {
                SetValue(OkCommandProperty, value);
            }
        }

        public static readonly DependencyProperty OkCommandProperty =
            DependencyProperty.Register("OkCommand", typeof(ICommand), typeof(RateUserControl), null);

        #endregion

        #region CancelCommand
        public ICommand CancelCommand
        {
            get
            {
                return (ICommand)GetValue(CancelCommandProperty);
            }
            set
            {
                SetValue(CancelCommandProperty, value);
            }
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(RateUserControl), null);

        #endregion

        private void RootPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsOpen = false;
        }
    }
}
