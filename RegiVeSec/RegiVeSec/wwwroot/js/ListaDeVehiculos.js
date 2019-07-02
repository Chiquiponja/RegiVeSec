var vm;

var dataTable;

function initVue() {
    vm = new Vue({
        el: '#app',
        data: {
          vehiculo: [],
          p_Desde: "",
          p_Hasta:""
        },
        methods: {
            obtenerVehiculo: function () {
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",


                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/Listar",
                })
                    .done(function (data) {

                        if (dataTable != null) {
                            dataTable.destroy();
                        }

                        vm.$data.vehiculo = data;

                        Vue.nextTick(function () {
                            dataTable = $('#example').DataTable({
                                dom: 'Bfrtip',
                                searching: true,
                                paging: true,
                                info: true,
                                sorting: false,
                                buttons: [
                                    { extend: 'excel', className: 'btn btn-primary' },
                                    { extend: 'pdf', className: 'btn btn-primary' }
                                ],
                                "language": {
                                    "zeroRecords": "No hay registros disponibles",
                                    "info": "Pagina _PAGE_ de _PAGES_",
                                    "infoEmpty": "No hay registros disponibles",
                                    "infoFiltered": "(filtered from _MAX_ total records)",
                                    "search": "Buscar: ",

                                }
                            });
                        })
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                    });

          },
          BuscarVehiculo:function() {


            var filtro = this.p_Desde + "|" + this.p_Hasta;
            $.ajax({
              //Cambiar a type: POST si necesario
              type: "GET",


              // Formato de datos que se espera en la respuesta
              dataType: "json",
              // URL a la que se enviará la solicitud Ajax
              url: "/Vehiculo/Buscar/" + filtro,
            })
                .done(function (data) {


                    if (dataTable != null) {
                        dataTable.destroy();
                    }

                    vm.$data.vehiculo = data;


                    Vue.nextTick(function () {
                        dataTable = $('#example').DataTable({
                            dom: 'Bfrtip',
                            searching: false,
                            paging: false,
                            info: false,
                            sorting: false,
                            buttons: [
                                { extend: 'excel', className: 'btn btn-primary' },
                                { extend: 'pdf', className: 'btn btn-primary' }
                            ],
                            "language": {
                                "zeroRecords": "No hay registros disponibles",
                                "info": "Pagina _PAGE_ of _PAGES_",
                                "infoEmpty": "No hay registros disponibles",
                                "infoFiltered": "(filtered from _MAX_ total records)",
                                "search": "Buscar: "
                                
                            }
                        });
                    })
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
