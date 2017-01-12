<?php
/* Login Codes */
include('Config.php');

/* Required Graph pictures/codes */
require_once ('../src/jpgraph.php');
require_once ('../src/jpgraph_line.php');
require_once ('../src/jpgraph_error.php');

/* Creates a connection and checks wether it's valid */
$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

/* Creates variables for usage */
$y_axis = array();
$x_axis = array();
$index = 0;
$sql = "SELECT * FROM sensor group by Date limit 24";
/* Gets the result from the SQL Query, when none is given it triggers an error */
$result = $conn->query($sql) or trigger_error($mysqli->error."[$sql]");

/* While there are more results from the Query, keeps filling the array */
while($row = mysqli_fetch_array($result)) {
$y_axis[$index] = $row["Humidity"]; // Gives the y_axis the Temperatures
$x_axis[$index] = $row["Date"]; // Gives the x_axis the Time
    $index++;
}

/* Graph Settings */
$graph = new Graph(1100,700); // Creates a new Graph with Width 1000 Pixels and Height 500 Pixels.
$graph->title->Set('Humidity'); // Set's the title for the graph
$graph->img->SetMargin(60,0,40,140);  // Margin from the Sides, all 40.
$graph->SetScale("textlin"); // Makes the Text Linear
$graph->SetShadow(); // Put's a small shadow underneath the line

$graph->xaxis->SetTickLabels($x_axis); // Sets the Labels on the X-Axis
$graph->xaxis->SetTitle("Time(Hours) ->",'center'); // Set's the title for the x-axis
$graph->xaxis->SetPos( 'min' ); // Set's the position for the lowest y value, so that the x-axis scales with that.
$graph->xaxis->SetLabelAngle(50);

$graph->yaxis->title->SetMargin(20); // Put's a small margin from the actual image.
$graph->yaxis->SetTitle("Percentage(%)", 'center');
$graph->yscale->SetGrace(5); // Used to make the Graph a bit larger than the maximum Y value.

/* Line Settings */
$p1 = new LinePlot($y_axis); // Add's the line, temperature, to the graph.

$p1->mark->SetType(MARK_SQUARE); // Make's the points(Temperatures) a square.
$p1->mark->SetFillColor("darkblue"); // Fills the square with darkblue
$p1->mark->SetWidth(5); // Sets the square width
$p1->SetColor("blue"); // Sets the line color
$p1->SetCenter(); // Centers the line.


$graph->Add($p1); // Add's the line to the graph.
$p1->value->Show(); // Show's the value's.
$graph->Stroke(); // Print's the graph.
?>
