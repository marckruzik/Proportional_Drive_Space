# Drive Space Proportional
Are you looking for the free space on one of your drives and want to see it immediately?
Most software displays all drives with the same visual size, making a 1 TB drive appear the same size as a 50 GB drive. This makes it difficult to get a clear sense of the actual available space on each drive.

**Drive Space Proportional** is a C# Polyglot Notebook that visualizes disk space in a proportional way. Its main use is to help users quickly identify drives with large amounts of free space, making it easier to store large files, copy datasets, install new video games, among other things.

![](image/clean.png)

## Tech stack
### Polyglot Notebook (VS Code extension)
Install **VS Code** with the Polyglot Notebook extension:

https://code.visualstudio.com/docs/languages/polyglot

### ScottPlot (nuget)
Used in the notebook, [ScottPlot](https://scottplot.net/) displays the chart.

This notebook is a good way to see how to use several aspects of ScottPlot:
* stacked bars: with their left and right positions
* vertical display
* explicit position for bars and ticks: to display data in the order provided by the code, as ScottPlot sets the first element at the bottom of the chart
* textual ticks: left axis, the drive names
* custom ticks: bottom axis, the total size in Gb
* custom legend: to create and display a legend independently from the bars code

### Custom calculations
The total height of the graph is automatically extended, depending on the unmber of drives.

## Shortcomings
* Small values are displayed in a space that is too small (see [complex example](image/complex.png)).
* Each group of bars is created separately (one drive = one group of bars), which limits the ability to apply shared data, such as the legend, to all bars of the same color.
