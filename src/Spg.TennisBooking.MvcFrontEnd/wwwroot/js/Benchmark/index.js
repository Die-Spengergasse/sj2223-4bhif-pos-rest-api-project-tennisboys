async function getBenchmarkDataSQL() {
    console.log("getBenchmarkDataSQL");

    await fetch("https://localhost:3001/api/v2/Benchmark/sql").then(async response => {
        const isJson = response.headers.get('content-type')?.includes('application/json');
        const data = isJson ? await response.json() : null;

        // check for error response
        if (!response.ok) {
            // get error message from body or default to response status
            const error = (data && data.message) || response.status;
            return Promise.reject(error);
        }
        console.log(data);

        let myData = JSON.parse(data);

        function generateTableBody(court) {
            var tableBody = ''

            for (var hour = 6; hour <= 20; hour++) {
                tableBody += '<tr>';
                tableBody += '<td>' + hour + ':00</td>'; // Hour column

                court.Days.forEach(function (day) {
                    var isReserved = day.Reservations.some(function (reservation) {
                        var fromHour = parseInt(reservation.From.split(':')[0]);
                        var toHour = parseInt(reservation.To.split(':')[0]);
                        return hour >= fromHour && hour < toHour;
                    });

                    var cellClass = isReserved ? 'reserved' : '';
                    tableBody += '<td class="' + cellClass + '"></td>';
                });

                tableBody += '</tr>';
            }

            return tableBody;
        }

        var sqlDiv = document.getElementById("sql");

        // Append a table for each court to the HTML body
        for (var i = 0; i < myData.Courts.length; i++) {
            console.log(myData.Courts[i]);
            //Create div
            var div = document.createElement("div");
            div.innerHTML += '<h2>' + myData.Courts[i].Name + '</h2>';
            div.innerHTML += '<table><thead><tr><th>Hour</th><th>Mo</th><th>Tu</th><th>We</th><th>Th</th><th>Fr</th><th>Sa</th><th>So</th></tr></thead><tbody>' + generateTableBody(myData.Courts[i]) + '</tbody></table>';
            //Append div
            sqlDiv.appendChild(div);
        };

    }).catch(error => {
        console.error('There was an error!', error);
    });
}

async function getBenchmarkDataMongo() {
    console.log("getBenchmarkDataMongo");

    await fetch("https://localhost:3001/api/v2/Benchmark/mongo").then(async response => {
        const isJson = response.headers.get('content-type')?.includes('application/json');
        const data = isJson ? await response.json() : null;

        // check for error response
        if (!response.ok) {
            // get error message from body or default to response status
            const error = (data && data.message) || response.status;
            return Promise.reject(error);
        }
        console.log(data);

        let myData = JSON.parse(data);

        function generateTableBody(court) {
            var tableBody = ''

            for (var hour = 6; hour <= 20; hour++) {
                tableBody += '<tr>';
                tableBody += '<td>' + hour + ':00</td>'; // Hour column

                court.Days.forEach(function (day) {
                    var isReserved = day.Reservations.some(function (reservation) {
                        var fromHour = parseInt(reservation.From.split(':')[0]);
                        var toHour = parseInt(reservation.To.split(':')[0]);
                        return hour >= fromHour && hour < toHour;
                    });

                    var cellClass = isReserved ? 'reserved' : '';
                    tableBody += '<td class="' + cellClass + '"></td>';
                });

                tableBody += '</tr>';
            }

            return tableBody;
        }

        var sqlDiv = document.getElementById("mongo");

        // Append a table for each court to the HTML body
        for (var i = 0; i < myData.Courts.length; i++) {
            console.log(myData.Courts[i]);
            //Create div
            var div = document.createElement("div");
            div.innerHTML += '<h2>' + myData.Courts[i].Name + '</h2>';
            div.innerHTML += '<table><thead><tr><th>Hour</th><th>Mo</th><th>Tu</th><th>We</th><th>Th</th><th>Fr</th><th>Sa</th><th>So</th></tr></thead><tbody>' + generateTableBody(myData.Courts[i]) + '</tbody></table>';
            //Append div
            sqlDiv.appendChild(div);
        };

    }).catch(error => {
        console.error('There was an error!', error);
    });
}

async function getBenchmarkData() {
    console.log("getBenchmarkData");
    getBenchmarkDataMongo();
    getBenchmarkDataSQL();
}

getBenchmarkData();