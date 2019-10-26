<?php
    require_once "uq/auth.php";             // Include UQ routines that handle UQ single-signon authentication
    auth_require();                         // User must authenticate once per session to continue running script
    $UQ = auth_get_payload();
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://www.amcharts.com/lib/4/core.js"></script>
    <script src="https://www.amcharts.com/lib/4/charts.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>
    <script src="js/statistics.min.js"></script>
    <script src="dynatable/jquery.dynatable.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" media="all" href="https://s3.amazonaws.com/dynatable-docs-assets/css/jquery.dynatable.css" />
    <link rel="stylesheet" media="all" href="https://s3.amazonaws.com/dynatable-docs-assets/css/bootstrap-2.3.2.min.css"> 
    <title>Statistics</title>
</head>
<body>
    <h1 id="heading">Statistics Page</h1>
    <p id="total_visits">Total visits: 0</p>
      
    <div id="datepickerdiv">
        <p>From Date: <input type="text" id="datepickerFrom"></p>
        <p>To Date: <input type="text" id="datepickerTo"></p>
        <input id="button" type="submit" name="button" onclick="myFunction();" value="Plot Graph"/>
    </div>
        
    <div id="chartdiv"></div>
    <h2 id="tableheading">Data Table</h2>
    <table id="table">
        <thead>
          <th>Name</th>
          <th>Email</th>
          <th>Message</th>
          <th>Date</th>
        </thead>
        <tbody>
        </tbody>
      </table>
</body>
</html>