namespace FsharpDesktop

open System
open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Avalonia.Controls.Shapes
open Avalonia.Threading
open FsharpDesktop
open Simulation




type MainWindow () as this = 
    inherit Window ()

    do this.InitializeComponent()
     

    member private this.InitializeComponent() =
#if DEBUG
        this.AttachDevTools()
#endif
        AvaloniaXamlLoader.Load(this)

        let mutable rec1 ,rec2, myCanvas,textBox = Rectangle(),Rectangle(),Canvas(),TextBox()
        rec1 <- this.FindControl<Rectangle>("rec1")
        rec2 <- this.FindControl<Rectangle>("rec2")
        myCanvas <- this.FindControl<Canvas>("myCanvas")
        textBox <- this.FindControl<TextBox>("Textbox")
        let Interval = TimeSpan(1L)
        let Simtimer = new DispatcherTimer()
        do Simtimer.Interval <- Interval
        do Simtimer.Tick.Add(fun eventArgs -> 
        Canvas.SetLeft(rec1,250. + Simulation.Point1.Position.Item(0)) 
        Canvas.SetBottom(rec1,250. + Simulation.Point1.Position.Item(1))
        Canvas.SetLeft(rec2,250. + Simulation.Point2.Position.Item(0)) 
        Canvas.SetBottom(rec2,250. + Simulation.Point2.Position.Item(1))
        SimLoop1 Point1
        Simloop2 Point2
        textBox.Text <- $" Point1 {Simulation.Point1.Acceleration.Item(0)} {Simulation.Point1.Acceleration.Item(1)}    "
        )
        Simtimer.Start()
