var allData = makeData(readFile())
var dict = makeDict(allData);

$(document).ready(function(){
    $("#total_visits").text("Total visits: " + getTotalVisits(dict));
    graph();
    $( "#datepickerFrom" ).datepicker();
    $( "#datepickerTo" ).datepicker();
    table();
});

function table() {
    var myRecords = makeData(readFile());
    $('#table').dynatable({
    dataset: {
        records: myRecords
    }
    });
}
function myFunction(){
    if (!document.getElementById("datepickerFrom").value && !document.getElementById("datepickerTo").value){
        graph();
        return
    }
    var tmpFrom = document.getElementById("datepickerFrom").value.split("/");
    var tmpTo = document.getElementById("datepickerTo").value.split("/");
    var newFrom = "".concat(tmpFrom[2]).concat("-").concat(tmpFrom[0]).concat("-").concat(tmpFrom[1]);
    var newTo = "".concat(tmpTo[2]).concat("-").concat(tmpTo[0]).concat("-").concat(tmpTo[1]);
    if (tmpFrom[2] == tmpTo[2]){
        // if years are same, compare months
        if (tmpFrom[0] == tmpTo[0]) {
            // if months are same, compare dates
            if (tmpFrom[1] <= tmpTo[1]) {
                graph(newFrom, newTo);
            } else {
                alert("Invalid inputs")
            }
        } else if (tmpFrom[2] < tmpTo[2]){
            // if month "from" before month "to", then valid
            graph(newFrom, newTo);
        } else {
            alert("Invalid inputs")
        }
    } else if (tmpFrom[2] < tmpTo[2]){
        // if year "from" before year "to", then valid
        graph(newFrom, newTo);
    } else {
        alert("Invalid inputs")
    }
};

function graph(from, to) {
    am4core.ready(function() {
        
        // Themes begin
        am4core.useTheme(am4themes_animated);
        // Themes end

        
        if(!from || !to) {
            var graphData = makeGraphData(dict)
        } else {
            var graphData = makeGraphDataFiltered(from, to, dict)
        }
        var chart = am4core.create("chartdiv", am4charts.XYChart);
        chart.hiddenState.properties.opacity = 0; // this creates initial fade-in


        chart.data = graphData;
        
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.renderer.grid.template.location = 0;

        categoryAxis.dataFields.category = "date";
        categoryAxis.renderer.minGridDistance = 40;
        categoryAxis.fontSize = 11;
        
        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.min = 0;
        valueAxis.max = getMaxVisits(dict);
        valueAxis.strictMinMax = true;
        valueAxis.renderer.minGridDistance = 30;

        
        var series = chart.series.push(new am4charts.ColumnSeries());
        series.dataFields.categoryX = "date";
        series.dataFields.valueY = "visits";
        series.columns.template.tooltipText = "{valueY.value}";
        series.columns.template.tooltipY = 0;
        series.columns.template.strokeOpacity = 0;
        
        // as by default columns of the same series are of the same color, we add adapter which takes colors from chart.colors color set
        series.columns.template.adapter.add("fill", function(fill, target) {
          return chart.colors.getIndex(target.dataItem.index);
        });
        
    }); // end am4core.ready()
}

function readFile() {
    var rawFile = new XMLHttpRequest();
    var allLines;
    rawFile.open("GET", "database.txt", false);
    rawFile.onreadystatechange = function ()
    {
        if(rawFile.readyState === 4)
        {
            if(rawFile.status === 200 || rawFile.status == 0)
            {
                var allText = rawFile.responseText;
                allLines = allText.split(/\r\n|\n/);
            }
        }
    }
    rawFile.send(null);
    return allLines;
}

function processLine(line) {
    var components = line.split("#");
    return components;
}

function makeData(allLines) {
    var data = []
    for (var i = 0; i < allLines.length; i++) {
        var processedLine = processLine(allLines[i]);
        data.push({"name": processedLine[0], "email": processedLine[1], "message": processedLine[2], "date": processedLine[3]});
    }
    return data;
}

function makeGraphData(dict) { 
    var graphData = []
    for (var key in dict) {
        graphData.push({"date": key, "visits": dict[key]});
    }
    return graphData;
}

function makeGraphDataFiltered(from, to, dict) { 
    var graphData = []
    for (var key in dict) {
        if (key >= from && key <= to) {
            graphData.push({"date": key, "visits": dict[key]});
        }
    }
    return graphData;
}

function getMaxVisits(dict) {
    var max = 0;
    for (var key in dict) {
        if (dict[key] > max) {
            max = dict[key]
        }
    }
    return max + 5;
}

function getTotalVisits(dict) {
    var ans = 0;
    for (var key in dict) {
        ans += dict[key]
    }
    return ans;
}

function makeDict(data) {
    var dict = {}
    for (var i = 0; i < data.length; i++) {
        if (dict[data[i]["date"]]) {
            dict[data[i]["date"]] += 1;
        } else {
            dict[data[i]["date"]] = 1;
        }
    }    
    return dict;
}