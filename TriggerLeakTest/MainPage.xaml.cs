using System.ComponentModel;
using System.Globalization;

namespace TriggerLeakTest;

public partial class MainPage : ContentPage
{
    readonly Label _heapSizeDisplay;

    public MainPage()
    {
        InitializeComponent();

        _heapSizeDisplay = new Label();

        var navButton = new Button
        {
            Text = "Nav"
        };
        navButton.Pressed += NavButtonOnPressed;

        var gcButton = new Button
        {
            Text = "GC"
        };
        gcButton.Pressed += GcButtonOnPressed;
        
        Content = new VerticalStackLayout
        {
            new Label
            {
                Text = "HeapSize MB"
            },
            _heapSizeDisplay,
            navButton,
            gcButton
        };
    }

    private void GcButtonOnPressed(object sender, EventArgs e)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        
        UpdateHeapDisplay();
    }

    void NavButtonOnPressed(object? sender, EventArgs e)
    {
        _ = Navigation.PushAsync(new HighMemoryPage());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        UpdateHeapDisplay();
    }

    private void UpdateHeapDisplay()
    {
        _heapSizeDisplay.Text = (GC.GetTotalMemory(false) / 1024d / 1024d).ToString(CultureInfo.InvariantCulture);
    }
}