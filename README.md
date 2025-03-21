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

* Stacked bars: with specified left and right positions.
* Vertical display.
* Explicit positioning for bars and ticks: ensuring data is displayed in the order provided by the code, as ScottPlot is designed to place the first element at the bottom of the chart.
* Textual ticks: on the left axis, displaying the drive names.
* Custom ticks: on the bottom axis, showing the total size in GB.
* Custom legend: for creating and displaying a legend independently from the bar code.

### Custom calculations
The total height of the graph is automatically adjusted based on the number of drives.

## Shortcomings
* Small values are displayed in a space that is too small (see [complex example](image/complex.png)).
* Each group of bars is created separately (one drive = one group of bars), which limits the ability to apply shared data, such as the legend, to all bars of the same color.
