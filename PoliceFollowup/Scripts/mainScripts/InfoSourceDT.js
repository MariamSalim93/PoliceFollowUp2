$(document).ready(function () {
    var oTable;
    $.ajax({
        url: "@Url.Action("GetListOfInfoSourceJson", "InfoSource")",
        type: "GET",
        dataType: "json",
        success: function (data) {
            oTable = $('#TableId').DataTable({
                data: data,
                responsive: true,
                columns: [
                    { data: "ReportNo", width: 70 },
                    {
                        data: "CreatedDate", width: 70,
                        render: function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                var match = data.match(/\/Date\((\d+)\)\//);
                                if (match) {
                                    var date = new Date(parseInt(match[1], 10));
                                    var formattedDate = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                                    return formattedDate;
                                } else {
                                    return "Invalid date";
                                }
                            }
                            return data;
                        }
                    },
                    { data: "FromDep" },
                    { data: "SecurityDegree" },
                    { data: "InfoSource" },
                    { data: "InfoStatus" },
                    {
                        'data': 'Status', width: 100,
                        render: function (data, type, row) {
                            if (data === "عادي") {
                                return '<div style="padding:2px 10px; background-color:#0a59a9; color:white; border-radius:6px; font-size:smaller; width:100px;text-align:center">' + data + '</div>';
                            } else if (data === "متوسط") {
                                return '<div style="padding:2px 10px; background-color:#ffb350; color:white; border-radius:6px; font-size:smaller; width:100px;text-align:center">' + data + '</div>';
                            } else if (data === "عالي") {
                                return '<div style="padding:2px 10px; background-color:#CF3131; color:white; border-radius:6px; font-size:smaller; width:100px;text-align:center">' + data + '</div>';
                            } else {
                                return data;
                            }
                        }
                    },
                    { data: "CreatedBy" },
                    {
                        data: null, width: 100,
                        render: function (data, type, row) {
                            var links = "";
                            links += "<a href='/InfoSource/Details/" + row.ReportNo + "' target='_blank' data-toggle='tooltip' data-placement='left' title='التفاصيل' style='text-decoration:none'><img src='/Content/images/View.png' width='24' /> </a>";
                            return links;
                        }
                    }
                ],
                order: [[0, 'desc']],
                language: {
                    search: "",
                    searchPlaceholder: " الاستعــلام",
                    sLengthMenu: "_MENU_ ",
                    info: "عرض _START_ إلى _END_ من اجمالي _TOTAL_ ",
                    infoFiltered: "(تصفية _MAX_ من الاجمالي)",
                    paginate: {
                        next: 'التالي',
                        previous: 'السابق'
                    }
                },
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'copy',
                        text: '<i class="fa fa-copy"></i> نسخ',
                        className: 'btn btn-sm buttons-copy'
                    },
                    {
                        extend: 'excel',
                        text: '<i class="fa fa fa-file-excel-o"></i> اكسل',
                        className: 'btn btn-sm buttons-excel'
                    },
                    {
                        extend: 'print',
                        text: '<i class="fa fa-print"></i> طباعة',
                        className: 'btn btn-sm buttons-print'
                    }
                ],
                initComplete: function () {
                    $('.dataTables_filter input').addClass('form-control');
                }
            });

            $('input', oTable.table().header()).on('keyup', function () {
                var colIdx = $(this).closest('th').index();
                oTable.column(colIdx).search(this.value).draw();
            });
        },
        error: function (xhr, status, error) {
            console.error("An error occurred:", status, error);
        }
    });
});
