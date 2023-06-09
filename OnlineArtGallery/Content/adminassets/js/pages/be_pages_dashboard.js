


One.onLoad(
    class {
        
        static initCharts() {
            (Chart.defaults.color = "#818d96"),
                (Chart.defaults.scale.grid.lineWidth = 0),
                (Chart.defaults.scale.beginAtZero = !0),
                (Chart.defaults.datasets.bar.maxBarThickness = 45),
                (Chart.defaults.elements.bar.borderRadius = 4),
                (Chart.defaults.elements.bar.borderSkipped = !1),
                (Chart.defaults.elements.point.radius = 0),
                (Chart.defaults.elements.point.hoverRadius = 0),
                (Chart.defaults.plugins.tooltip.radius = 3),
                (Chart.defaults.plugins.legend.labels.boxWidth = 10);
            let r,
                a,
                o,
                t,
                e = document.getElementById("js-chartjs-earnings"),
                l = document.getElementById("js-chartjs-total-orders"),
                n = document.getElementById("js-chartjs-total-earnings"),
                s = document.getElementById("js-chartjs-new-customers");
            null !== e &&
                (r = new Chart(e, {
                    type: "bar",
                    data: {
                        labels: ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"],
                        datasets: [
                            {
                                label: "Last Year",
                                fill: !0,
                                backgroundColor: "rgba(100, 116, 139, .7)",
                                borderColor: "transparent",
                                pointBackgroundColor: "rgba(100, 116, 139, 1)",
                                pointBorderColor: "#fff",
                                pointHoverBackgroundColor: "#fff",
                                pointHoverBorderColor: "rgba(100, 116, 139, 1)",
                                data: JSON.parse(localStorage.getItem('revenue2022')),
                            },
                            {
                                label: "This Year",
                                fill: !0,
                                backgroundColor: "rgba(100, 116, 139, .15)",
                                borderColor: "transparent",
                                pointBackgroundColor: "rgba(100, 116, 139, 1)",
                                pointBorderColor: "#fff",
                                pointHoverBackgroundColor: "#fff",
                                pointHoverBorderColor: "rgba(100, 116, 139, 1)",
                                data: JSON.parse(localStorage.getItem('revenue2023')),
                            },
                        ],
                    },
                    options: {
                        scales: { x: { display: !1, grid: { drawBorder: !1 } }, y: { display: !1, grid: { drawBorder: !1 } } },
                        interaction: { intersect: !1 },
                        plugins: {
                            legend: { labels: { boxHeight: 10, font: { size: 14 } } },
                            tooltip: {
                                callbacks: {
                                    label: function (r) {
                                        return r.dataset.label + ": $" + r.parsed.y;
                                    },
                                },
                            },
                        },
                    },
                })),
                null !== l &&
                (a = new Chart(l, {
                    type: "line",
                    data: {
                        labels: ["MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"],
                        datasets: [
                            {
                                label: "Total Orders",
                                fill: !0,
                                backgroundColor: "rgba(220, 38, 38, .15)",
                                borderColor: "transparent",
                                pointBackgroundColor: "rgba(220, 38, 38, 1)",
                                pointBorderColor: "#fff",
                                pointHoverBackgroundColor: "#fff",
                                pointHoverBorderColor: "rgba(220, 38, 38, 1)",
                                data: [33, 29, 32, 37, 38, 30, 34, 28, 43, 45, 26, 45, 49, 39],
                            },
                        ],
                    },
                    options: {
                        maintainAspectRatio: !1,
                        tension: 0.4,
                        scales: { x: { display: !1 }, y: { display: !1 } },
                        interaction: { intersect: !1 },
                        plugins: {
                            legend: { display: !1 },
                            tooltip: {
                                callbacks: {
                                    label: function (r) {
                                        return " " + r.parsed.y + " Orders";
                                    },
                                },
                            },
                        },
                    },
                })),
                null !== n &&
                (o = new Chart(n, {
                    type: "line",
                    data: {
                        labels: ["MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"],
                        datasets: [
                            {
                                label: "Total Earnings",
                                fill: !0,
                                backgroundColor: "rgba(101, 163, 13, .15)",
                                borderColor: "transparent",
                                pointBackgroundColor: "rgba(101, 163, 13, 1)",
                                pointBorderColor: "#fff",
                                pointHoverBackgroundColor: "#fff",
                                pointHoverBorderColor: "rgba(101, 163, 13, 1)",
                                data: [716, 1185, 750, 1365, 956, 890, 1200, 968, 1158, 1025, 920, 1190, 720, 1352],
                            },
                        ],
                    },
                    options: {
                        maintainAspectRatio: !1,
                        tension: 0.4,
                        scales: { x: { display: !1 }, y: { display: !1 } },
                        interaction: { intersect: !1 },
                        plugins: {
                            legend: { display: !1 },
                            tooltip: {
                                callbacks: {
                                    label: function (r) {
                                        return " $" + r.parsed.y;
                                    },
                                },
                            },
                        },
                    },
                })),
                null !== s &&
                (t = new Chart(s, {
                    type: "line",
                    data: {
                        labels: ["MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"],
                        datasets: [
                            {
                                label: "Total Orders",
                                fill: !0,
                                backgroundColor: "rgba(101, 163, 13, .15)",
                                borderColor: "transparent",
                                pointBackgroundColor: "rgba(101, 163, 13, 1)",
                                pointBorderColor: "#fff",
                                pointHoverBackgroundColor: "#fff",
                                pointHoverBorderColor: "rgba(101, 163, 13, 1)",
                                data: [25, 15, 36, 14, 29, 19, 36, 41, 28, 26, 29, 33, 23, 41],
                            },
                        ],
                    },
                    options: {
                        maintainAspectRatio: !1,
                        tension: 0.4,
                        scales: { x: { display: !1 }, y: { display: !1 } },
                        interaction: { intersect: !1 },
                        plugins: {
                            legend: { display: !1 },
                            tooltip: {
                                callbacks: {
                                    label: function (r) {
                                        return " " + r.parsed.y + " Customers";
                                    },
                                },
                            },
                        },
                    },
                }));
        }
        static init() {
            this.initCharts();
        }
    }.init()
);
