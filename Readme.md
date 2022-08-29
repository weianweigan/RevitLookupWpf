
## Revit Lookup WPF

![Revit API](https://img.shields.io/badge/Revit%20API%202022-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgray.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

![ReSharper](https://img.shields.io/badge/ReSharper-2021.3.2-yellow)
![Rider](https://img.shields.io/badge/Rider-2021.3.2-yellow)
![Visual Studio 2022](https://img.shields.io/badge/Visual_Studio_2022_Preview_2.0-17.1.0-yellow)
![.NET Framework](https://img.shields.io/badge/.NET_6-yellow)

[![Publish](../../actions/workflows/Workflow.yml/badge.svg)](../../actions)
[![Github All Releases](https://img.shields.io/github/downloads/weianweigan/RevitLookupWpf/total?color=blue&label=Download)]()
[![HitCount](https://hits.dwyl.com/weianweigan/RevitLookupWpf.svg?style=flat-square)](http://hits.dwyl.com/weianweigan/RevitLookupWpf)

# Introduction

Interactive Revit RFA and RVT project database exploration tool to view and navigate BIM element parameters, properties and relationships.The project was developed to support programmers using Revit API to quickly look up and work efficiently with parameter objects.

The advantage of project RevitLookupWpf is that it allows the user to delve into full methods and properties, which makes it easier for professionals to discover revit's hidden functions.

![Main Interface of Revit Lookup Wpf](./pic/window.png)

## Installation

Please follow last release at section [Release](https://github.com/weianweigan/RevitLookupWpf/releases/latest)

**Note:** The release currently supports 5 version Revit : 2019, 2020, 2021, 2022 , 2023

---
### Revit Add-in

With RevitlookupWpf inside, you can use some features like:

- [x] Snoop DB : Explore the database of Revit API.
- [x] Snoop Active Document : Explore the active document of Revit project current.
- [x] Snoop Active View : Explore the active view of Revit project current.
- [x] Snoop Current Selection : Explore the element current selected of Revit project current.
- [x] Snoop Points : Explore the some points by pick select in project.
- [x] Snoop Faces : Explore some faces contains inside Geometry Element.
- [x] Snoop Edges : Explore some edges contains inside Geometry Element.
- [x] Snoop Points On Elements : Explore the points on Element by pick select Points. 
- [x] Snoop Geometry Element  : Explore Geometry Element.
- [x] Snoop Linked Element : Explore all Element inside Revit Linked project.
- [x] Snoop UIApplication : Explore UIApplication Revit project.
- [x] Snoop Search Elements  :Explore all element in document current and linked document by Id. 

![](pic/SnoopElement.gif)

### Dynamo Revit Package

Happy to say that RevitLookupWpf also support snoop everything inside inviroment Dynamo Revit. Some feature will be support included:

- [x] Snoop Revit Element (**Dynamo Element** _Wraped_ from **Revit Element**)
- [x] Snoop Current Selection
- [x] Snoop Active Document 
- [x] Snoop Active View
- [x] Snoop Object (Everything objects defined in Dynamo)

![](pic/DynamoSnoop.gif)

**Note** : Please download package **RevitlookupWpf** And package **DynamoIronPython2.7** before starting Snoop in enviroment Dynamo Revit. 

### Advanced

- Allow show help information **Properties** and **Methods** :

![](pic/Help.gif)

- Connect with [RevitAPIDocs](https://www.revitapidocs.com/)

![](pic/Revitapidocs.gif)

- Set Value input method require input parameters

![](pic/SetInputMethod.gif)

## Author

Originally implemented by [weianweigan](https://github.com/weianweigan), contribute with [Chuong Ho](https://github.com/chuongmep).

---

## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [License](License.md) file for full details.

---

## Contribute

**Revit Lookup WPF** is an open-source project and would be nothing without its community. You can make suggestions or track and submit bugs via Github [issues](https://docs.github.com/en/issues/tracking-your-work-with-issues/creating-an-issue). You can submit your own code to the **Revit Lookup WPF** project via a Github [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests).

Many Thanks all contributors for this repository. Feel free to contribute!
Please refer to the [CONTRIBUTING](CONTRIBUTING.md) for details.

<a href = "https://github.com/weianweigan/RevitLookupWpf/graphs/contributors">
  <img src = "https://contrib.rocks/image?repo=weianweigan/RevitLookupWpf"/>
</a>

---

## Sponsors

Thanks to [JetBrains](https://www.jetbrains.com/) for providing licenses for [Rider](https://www.jetbrains.com/rider/) and [dotUltimate](https://www.jetbrains.com/dotnet/) tools, which both make open-source development a real pleasure!

---
