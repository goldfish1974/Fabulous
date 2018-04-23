﻿// Copyright 2018 Elmish.XamarinForms contributors. See LICENSE.md for license.
namespace Elmish.XamarinForms.DynamicViews 

open Xamarin.Forms

open Elmish.XamarinForms
open Elmish.XamarinForms.DynamicViews
[<AutoOpen>]
module SimplerHelpers = 
    open System.Runtime.CompilerServices
    open System.Collections.Generic
    open System.Windows.Input


    let rowdef h = Xaml.RowDefinition(height=makeGridLength h)
    let coldef h = Xaml.ColumnDefinition(width=makeGridLength h)

    let rows rds (els: XamlElement list) = 
        let children = els |> List.mapi (fun i x -> x.GridRow i)
        Xaml.Grid(rowdefs=rds, children=children)

    let cols cds (els: XamlElement list) = 
        let children = els |> List.mapi (fun i x -> x.GridColumn i)
        Xaml.Grid(coldefs=cds, children=children)


    // Helper page for the TicTacToe sample
    // Need to generlize the HeightRequest phase of the XF content digestion process...
    type HelperPage(?viewAllocatedSizeFixup) as self =
        inherit ContentPage()
        do Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(self, true)

        // painful.... It is unfortunately not possible to simpy recreate the whole
        // view here, you have to mutate the content in-place.
        override this.OnSizeAllocated(width, height) =
            base.OnSizeAllocated(width, height)
            match viewAllocatedSizeFixup with 
            | Some f -> f self.Content (width, height)
            | None -> ()

