//var vm;
//document.addEventListener('DOMContentLoaded', function () {
  var  vm = new Vue({
        el: '#appAutos',
        data: {

            vehiculo: {
                Id: 0,
                FechaDeIngreso: '',
                Propietario: '',
                Dominio: '',
                Tipo: '',
                Marca: '',
                Color: '',
                Modelo: '',
                Causa: '',
                Estado: '',
                NumeroSumario: '',
                Deposito: '',
                Orden: '',
                DependenciaProcedente: '',
                Observaciones: '',
                Recibe: '',
                Entrega: '',
                MagistradoInterviniente: '',
                SumarioRegistrar: '',
                UbicacionActual: '',
                FechaDeEntrega: '',
                TipoId: 0,
                foto: "",
                EstadoId: 0,
                ImagenesPorVehiculo: [],

            },

            Autos: [],
            Tipos: [],
            Estados: [],

        },

      methods: {

          DeleteItem:function(list, index) {
              list.splice(index, 1);
          },
            crearvehiculo: function () {

                var data = vm.$data.vehiculo;
                $.ajax({
                    url: "/Vehiculo/Add",
                    contentType: "application/json",
                    async: true,
                    type: "POST",
                    data: JSON.stringify(data),
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
            ObtenerEstados: function () {
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",
                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/GetEstados",
                })
                    .done(function (data) {
                        vm.$data.Estados = data;
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud de Estados de contacto ha fallado: " + textStatus);
                        }
                    });
            },
            EditarVehiculo: function () {

                var data = vm.$data.vehiculo;
                $.ajax({
                    url: "/Vehiculo/Edit",
                    contentType: "application/json",
                    async: true,
                    type: "POST",
                    data: JSON.stringify(data),
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("FAIL: " + errorThrown);
                    },
                    success: function (data, textStatus, jqXHR) {
                        console.log("SUCCESS!");
                    }
                });
            },


            //obtenerAutoById: function () {

            //    var data = vm.$data.vehiculo;
            //    $.ajax({
            //        url: "/Vehiculo/Details",
            //        contentType: "application/json",
            //        async: true,
            //        type: "GET",
            //        data: JSON.stringify(data),
            //        error: function (jqXHR, textStatus, errorThrown) {
            //            console.log("FAIL: " + errorThrown);
            //        },
            //        success: function (data, textStatus, jqXHR) {
            //            console.log("SUCCESS!");
            //        }
            //    });
            //},
            obtenerAutoById: function (id) {
                $.ajax({
                    //Cambiar a type: POST si necesario
                    type: "GET",
                    // Formato de datos que se espera en la respuesta
                    dataType: "json",
                    // URL a la que se enviará la solicitud Ajax
                    url: "/Vehiculo/GetDtoById/" + id,
                })
                    .done(function (data) {
                        vm.$data.vehiculo.Vehiculo = data.vehiculo;
                        vm.$data.vehiculo.Id = data.id;
                        vm.$data.vehiculo.Causa = data.causa;
                        vm.$data.vehiculo.Color = data.color;
                        vm.$data.vehiculo.DependenciaProcedente = data.dependenciaProcedente;
                        vm.$data.vehiculo.Deposito = data.deposito;
                        vm.$data.vehiculo.Dominio = data.dominio;
                        vm.$data.vehiculo.Entrega = data.entrega;
                        vm.$data.vehiculo.Estado = data.estado;
                        vm.$data.vehiculo.EstadoId = data.estadoId;
                        vm.$data.vehiculo.FechaDeEntrega = data.fechaDeEntrega;
                        vm.$data.vehiculo.FechaDeIngreso = data.fechaDeIngreso;
                        vm.$data.vehiculo.ImagenesPorVehiculo = data.imagenesPorVehiculo;
                        vm.$data.vehiculo.Marca = data.marca;
                        vm.$data.vehiculo.MagistradoInterviniente = data.magistradoInterviniente;
                        vm.$data.vehiculo.Modelo = data.modelo;
                        vm.$data.vehiculo.NumeroSumario = data.numeroSumario;
                        vm.$data.vehiculo.Observaciones = data.obtenerAutoById;
                        vm.$data.vehiculo.ObtenerEdit = data.obtenerEdit;
                        vm.$data.vehiculo.ObtenerEstados = data.obtenerEstados;
                        vm.$data.vehiculo.ObtenerTipos = data.obtenerTipos;
                        vm.$data.vehiculo.Orden = data.orden;
                        vm.$data.vehiculo.Observaciones = data.observaciones;
                        vm.$data.vehiculo.Propietario = data.propietario;
                        vm.$data.vehiculo.Recibe = data.recibe;
                        vm.$data.vehiculo.SumarioRegistrar = data.sumarioRegistrar;
                        vm.$data.vehiculo.Tipo = data.tipo;
                        vm.$data.vehiculo.TipoId = data.tipoId;
                        vm.$data.vehiculo.Tipos = data.tipos;
                        vm.$data.vehiculo.UbicacionActual = data.ubicacionActual;

                        vm.$forceUpdate();
                        return true;
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        if (console && console.log) {
                            console.log("La solicitud ha fallado: " + textStatus);
                        }
                        return false;
                    });
          },
          obtenerExportToPdfDetallesById: function (id) {
              $.ajax({
                  //Cambiar a type: POST si necesario
                  type: "POST",
                  // Formato de datos que se espera en la respuesta
                  dataType: "json",
                  // URL a la que se enviará la solicitud Ajax
                  url: "/Vehiculo/ExportToPdfDetalles/" + id,
              })

          },
           
        }
    }
    );
//});
