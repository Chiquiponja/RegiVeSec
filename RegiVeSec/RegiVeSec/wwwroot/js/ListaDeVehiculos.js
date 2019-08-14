var vm;

var dataTable;

function initVue() {
    vm = new Vue({
        el: '#app',
        data: {
            vehiculo: [],
            p_Desde: "",
            p_Hasta: "",
            p_Texto: ""
        },
        methods: {
            obtenerVehiculo: function () {
                vm.$data.p_Desde = "";
                vm.$data.p_Hasta = "";
                vm.$data.p_Texto = "";

                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",
                   

                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/Listar",
                })
                    .done(function (data) {

                        vm.$data.vehiculo = data;
                        if (dataTable != null) {
                            dataTable.destroy();
                        }
                        var jsonData = data;
                        Vue.nextTick(function () {
                            initDataTable();
                        })
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                    });
            },
            BuscarVehiculo: function () {


                var filtro = this.p_Texto + "|" + this.p_Desde + "|" + this.p_Hasta;
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",


                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/Buscar/" + filtro,
                })
                    .done(function (data) {


                        //if (dataTable != null) {
                        //    dataTable.destroy();
                        //}

                        console.log(data);
                        if (dataTable != null) {
                            dataTable.destroy();
                        }
                        vm.$data.vehiculo = data;
                        Vue.nextTick(function () {
                            initDataTable();
                        })

                        //Vue.nextTick(function () {
                        //    dataTable = $('#TablaVehiculos').DataTable({
                        //        dom: 'Bfrtip',
                        //        searching: false,
                        //        paging: true,
                        //        info: false,
                        //        sorting: false,
                        //        "serverSide": true,
                        //        "ajax": {
                        //            "url": "/Vehiculo/Tabla",
                        //            "type": "POST",
                        //            "datatype": "json"
                        //        },
                        //        //"processing": "true",
                        //        buttons: [
                        //            { extend: 'excel', className: 'btn btn-primary' },
                        //            { extend: 'pdf', className: 'btn btn-primary' }
                        //        ],
                        //        "language": {
                        //            "zeroRecords": "No hay registros disponibles",
                        //            "info": "Pagina _PAGE_ of _PAGES_",
                        //            "infoEmpty": "No hay registros disponibles",
                        //            "infoFiltered": "(filtered from _MAX_ total records)",
                        //            "search": "Buscar: "

                        //        }
                        //    });
                        //})
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                    });
            }

        }
    })
};

function initDataTable(){
    dataTable = $('#TablaVehiculos').DataTable({
       
        dom: 'Bfrtip',
        searching: false,
        paging: true,
        info: true,
        sorting: true,
        //order: [0, "asc"],
        //data: jsonData,
        //"serverSide": true,
        //"ajax": {
        //    "url": "/Vehiculo/Tabla",
        //    "type": "POST",
        //    "datatype": "json"
        //},
        //"processing": true,
        buttons: [
            {
                extend: 'excel', className: 'btn btn-primary', exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            },
            {
                extend: 'pdf', className: 'btn btn-primary',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            }
        ],
        "language": {
            "zeroRecords": "No hay registros disponibles",
            "info": "Pagina _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros disponibles",
            "infoFiltered": "(filtered from _MAX_ total records)",
            "search": "Buscar: ",
            "next": "Siguiente",
            "previous": "Anterior"

        },
    },
    );
}
