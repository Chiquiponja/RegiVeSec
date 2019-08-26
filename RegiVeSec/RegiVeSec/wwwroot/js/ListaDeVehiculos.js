var vm;

var dataTable;

function initVue() {
    vm = new Vue({
        el: '#app',
        data: {
            vehiculo: [],
            p_PaginaActual: 1,
            p_TotalRegistros: 0,
            p_Desde: "",
            p_Hasta: "",
            p_Texto: "",
            p_Lista: ""
        },

        computed: {

            NumeroPaginaParaPrimerBoton: function() {
                if (this.p_PaginaActual < 3)
                    return 1;
                else
                    return this.p_PaginaActual - 1;
            },

            NumeroPaginaParaSegundoBoton: function () {
                if (this.p_PaginaActual < 3)
                    return 2;
                else
                    return this.p_PaginaActual;
            },

            NumeroPaginaParaTercerBoton: function () {
                if (this.p_PaginaActual < 3)
                    return 3;
                else
                    return this.p_PaginaActual + 1;
            }
        },

      methods: {


        limitarPaginacion:function (boton){

          var cant = parseInt(this.p_TotalRegistros / 5);
          if (this.p_TotalRegistros % 5 > 0) cant++;

          return cant < boton;

        },

            obtenerVehiculo: function (numeroPagina) {
                vm.$data.p_Desde = "";
                vm.$data.p_Hasta = "";
                vm.$data.p_Texto = "";

                var nroPagina = numeroPagina;

                $.getJSON("/Vehiculo/Listar?paginaActual=" + numeroPagina)
                    .done(function (data) {

                        vm.$data.vehiculo = data.vehiculos;
                        vm.$data.p_TotalRegistros = data.totalRegistros;
                        vm.$data.p_PaginaActual = nroPagina;
                        
                        if (dataTable != null) {
                            dataTable.destroy();
                        }
                        //var jsonData = data;
                        Vue.nextTick(function () {
                            initDataTable();
                        })
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                    });
                    

                //$.ajax({
                //    //Cambiar a type: POST si necesario
                //    type: "GET",
                   

                //    // Formato de datos que se espera en la respuesta
                //    dataType: "json",
                //    // URL a la que se enviará la solicitud Ajax
                //    url: ,
                //})
                    
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
        paging: false,
        info: true,
        sorting: true,
        //order: [0, "asc"],
        //data: jsonData,
        ////"serverSide": true,
        //"ajax": {
        //    "url": "/Vehiculo/Products",
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
