using ScottPlot;
using ScottPlot.TickGenerators; // To have custom bottom ticks

namespace DrivePlot
{
    public record Drive
    {
        public string letter = "0";
        public string label = "noname";
        public int space_used_gb;
        public int space_free_gb;

        public int space_total => space_used_gb + space_free_gb;
    }

    public static class Plot_Generator
    {
        public static string from_width_get_filepath(int width)
        {
            List<Drive> list_drive = get_list_drive();
            var plot = from_list_drive_get_plot(list_drive);

            int height = 60 + list_drive.Count * (40 + 10) + 110;

            string tmpPath = Path.GetTempFileName();
            plot.SavePng(tmpPath, width, height);

            return tmpPath;
        }

        internal static Plot from_list_drive_get_plot(List<Drive> list_drive)
        {
            Plot plot = new();
            plot.Title("Proportional Drive Space");

            //--------------------------------------------------------
            // Bars
            //--------------------------------------------------------
            Color colorEmpty = Color.FromHex("E8E8E8");
            Color colorFull = Color.FromHex("25A0DC");

            int pi(int i) => list_drive.Count - 1 - i;

            Bar[] bars =
                list_drive.SelectMany((disk, i) => new[]
                {
                    new Bar
                    {
                        Position = pi(i),
                        ValueBase = 0, // column left position
                        Value = disk.space_free_gb, // column right position
                        FillColor = colorEmpty,
                        Label = $"{disk.space_free_gb}Gb",
                        CenterLabel = true,
                    },
                    new Bar
                    {
                        Position = pi(i),
                        ValueBase = disk.space_free_gb, // column left position
                        Value = disk.space_total, // column right position
                        FillColor = colorFull,
                        Label = $"{disk.space_used_gb}Gb",
                        CenterLabel = true,
                    },
                }
                ).ToArray();

            var barPlot = plot.Add.Bars(bars);
            barPlot.Horizontal = true;

            //--------------------------------------------------------
            // Left Ticks
            //--------------------------------------------------------
            Tick[] ticksLeft =
                list_drive
                .Select((disk, i) => new Tick(pi(i), $"{disk.letter}\r\n{disk.label}"))
                .ToArray();

            plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticksLeft);
            plot.Axes.Left.MajorTickStyle.Length = 0;

            //--------------------------------------------------------
            // Bottom ticks
            //--------------------------------------------------------
            plot.Axes.Bottom.TickGenerator = new NumericAutomatic
            {
                LabelFormatter = (double position) => $"{position} Gb"
            };

            //--------------------------------------------------------
            // Legend
            //--------------------------------------------------------
            plot.Legend.IsVisible = true;
            plot.Legend.Alignment = Alignment.UpperRight;
            plot.Legend.ManualItems.Add(new() { LabelText = "Free space", FillColor = colorEmpty });
            plot.Legend.ManualItems.Add(new() { LabelText = "Used space", FillColor = colorFull });

            plot.ShowLegend(Edge.Bottom);

            //--------------------------------------------------------
            // Display
            //--------------------------------------------------------
            plot.HideGrid();
            plot.Axes.Margins(left: 0);

            return plot;
        }

        internal static List<Drive> get_list_drive()
        {
            int from_byte_to_gb(long byt) => (int)(byt / Math.Pow(1024, 3));

            List<Drive> list_drive =
                DriveInfo
                .GetDrives()
                .Where(d => d.IsReady == true)
                .Select(drive =>
                {
                    try
                    {
                        return new Drive
                        {
                            letter = drive.Name,
                            label = drive.VolumeLabel,
                            space_used_gb = from_byte_to_gb(drive.TotalSize - drive.AvailableFreeSpace),
                            space_free_gb = from_byte_to_gb(drive.AvailableFreeSpace)
                        };
                    }
                    catch (IOException)
                    {
                        return new Drive
                        {
                            letter = drive.Name,
                            label = "Unknown",
                            space_used_gb = 0,
                            space_free_gb = 0
                        };
                    }
                }
            ).ToList();
            return list_drive;
        }
    }
}
