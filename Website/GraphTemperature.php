<?php
/* Login Codes */
$servername = "localhost";
$username = "weatherstation";
$password = "weerstation";
$dbname = "weatherr";

/* Required Graph pictures/codes */
require_once ('src/jpgraph.php');
require_once ('src/jpgraph_line.php');
require_once ('src/jpgraph_error.php');

/* Creates a connection and checks wether it's valid */
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

/* Creates variables for usage */
$y_axis = array();
$x_axis = array();
$index = 0;
$sql = "SELECT * FROM sensor group by Date";
/* Gets the result from the SQL Query, when none is given it triggers an error */
$result = $conn->query($sql) or trigger_error($mysqli->error."[$sql]");

/* While there are more results from the Query, keeps filling the array */
while($row = mysqli_fetch_array($result)) {
$y_axis[$index] = $row["Temperature"]; // Gives the y_axis the Temperatures
$x_axis[$index] = $row["Date"]; // Gives the x_axis the Time
    $index++;
}

/* Graph Settings */
$graph = new Graph(1100,500); // Creates a new Graph with Width 1000 Pixels and Height 500 Pixels.
$graph->img->SetMargin(60,0,40,40);  // Margin from the Sides, all 40.
$graph->img->SetAntiAliasing();
$graph->SetScale("textlin"); // Makes the Text Linear
$graph->SetShadow();
$graph->xaxis->SetTickLabels($x_axis); // Sets the Labels on the X-Axis
$graph->yaxis->scale->SetAutoMin(0); // Sets the minimum Value to 0, not neccesarily needed.
$graph->xaxis->SetTitle("Time(Hours) ->",'center');
$graph->yaxis->SetTitle("Temperature(Celcius)", 'center');
$graph->title->Set('Temperature');
$graph->yaxis->title->SetMargin(20);
$graph->title->SetFont(FF_ARIAL,FS_NORMAL,15);
$graph->yscale->SetGrace(5); // Used to make the Graph a bit larger than the maximum Y value.

/* Line Settings */
$p1 = new LinePlot($y_axis);
$p1->mark->SetType(MARK_FILLEDCIRCLE);
$p1->mark->SetFillColor("red");
$p1->mark->SetWidth(3);
$p1->SetColor("blue");
$p1->SetCenter();

$graph->Add($p1);
$graph->Stroke();
?>