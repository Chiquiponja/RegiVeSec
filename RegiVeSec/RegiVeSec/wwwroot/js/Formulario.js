var vm;
document.addEventListener('DOMContentLoaded', function () {
    vm = new Vue({
        el: '#appAutos',
        data: {

            vehiculo: {
                Id: 0,
               FechaDeIngreso: '',
                Propietario: '',
                Dominio: '',
                Tipo: '',
                Marca : '',
                Color : '',
                Modelo: '',
                Causa : '',
                Estado: '',
                NumeroSumario: '',
                Dependencia: '',
                Orden: '',
                DependenciaProcedente: '',
                Observaciones: '',
                Recibe: '',
                Entrega: '',
                FechaDeEntrega: ''
            },

            Autos: [],
            Tipos: []
        },
        methods: {
            crearvehiculo: function () {

                $.ajax({
                    url: "/Vehiculo/Add",
                    contentType: "application/json",
                    async: true,
                    type: "POST",
                    data: data,
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("FAIL: " + errorThrown);
                    },
                    success: function (data, textStatus, jqXHR) {
                        console.log("SUCCESS!");
                    }
                });
            },
            ObtenerTipos: function () {
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",
                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/GetTipos",
                })
                    .done(function (data) {
                        vm.$data.Tipos = data;
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud de tipos de contacto ha fallado: " + textStatus);
                        }
                    });
            },
            obtenerAutoById: function (id) {
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",
                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/GetVehiculoRegiVeSecId/" + id,
                })
                    .done(function (data) {
                        vm.$data.vehiculo = data;
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                    });
            },
        }
    }
    )
});