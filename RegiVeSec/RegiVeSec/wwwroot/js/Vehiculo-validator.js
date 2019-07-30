
$().ready((function () {
    
    $("#vehiculoform").validate({


        rules: {


            Propietario  : {
            required: true,
            minlength: 3

            },
            FechaDeIngreso: {
                required: true,
                minlength: 3

            },
            Dominio:   {
            required: true,
            minlength: 3

        },
        //    Tipo: {
        //    required: true,
        //    minlength: 3

        //},
            Marca: {
            required: true,
            minlength: 3

        },
            Color:  {
            required: true,
            minlength: 3

        },
            Modelo: {
            required: true,
            minlength: 3

        },
            Causa: {
            required: true,
            minlength: 3

        },
            Estado: {
            required: true,
            minlength: 3

        },
            NumeroSumario:  {
            required: true,
            minlength: 3

        },
            Orden:  {
            required: true,
            minlength: 3

        },
            DependenciaProcedente:  {
            required: true,
            minlength: 3

        },
            Observaciones:  {
            required: true,
            minlength: 3

        },
            Recibe: {
            required: true,
            minlength: 3

            },
            Dependencia: {
                required: true,
                minlength: 3

            },
            DependenciaRecibe: {
                required: true,
                minlength: 3

            },
            FechaDeEntrega: {
                required: true,
                minlength: 3
            },
            Entrega:  {
            required: true,
            minlength: 3

            },
            Nombre: {
                required: true,
                minlength: 3
            },
            Contrasenia: {
                required: true,
                minlength: 8
            }
        

        },
        messages: {
            FechaDeIngreso: {
                required: "Escribi tu Fecha... ",
                minlength: "Minimo 3 caracter"
            },
            Propietario: {
                required: "Escribi tu Propietario... ",
                minlength: "Minimo 3 caracter"
            },
            Dominio: {
                required: "Escribi tu Dominio... ",
                minlength: "Minimo 3 caracter"

            },
            //Tipo: {
            //    required: "Escribi tu Tipo... ",
            //    minlength: "Minimo 3 caracter"

            //},
            Marca: {
                required: "Escribi tu Marca... ",
                minlength: "Minimo 3 caracter"

            },
            Dependencia: {
                required: "Escribi tu Dependencia... ",
                minlength: "Minimo 3 caracter"

            },
            Color: {
                required: "Escribi el Color... ",
                minlength: "Minimo 3 caracter"

            },
            Modelo: {
                required: "Escribi tu Modelo... ",
                minlength: "Minimo 3 caracter"

            },
            Causa: {
                required: "Escribi la Causa... ",
                minlength: "Minimo 3 caracter"

            },
            Estado: {
                required: "Escribi tu Estado... ",
                minlength: "Minimo 3 caracter"

            },
            NumeroSumario: {  
                required: "Escribi tu Numero Sumario... ",
                minlength: "Minimo 3 caracter"

            },
            Orden: {
                required: "Escribi tu Orden... ",
                minlength: "Minimo 3 caracter"

            },
            DependenciaProcedente: {
                required: "Escribi la Dependencia Procedente... ",
                minlength: "Minimo 3 caracter"

            },
            Observaciones: {
                required: "Escribi tu  Observaciones... ",
                minlength: "Minimo 3 caracter"
            },
            Recibe: {
                required: "Escribi quien Recibe... ",
                minlength: "Minimo 3 caracter"

            },
            Dependencia: {
                required: "Escribi la Dependencia... ",
                minlength: "Minimo 3 caracter"

            },
            FechaDeEntrega: {
                required: "Escribi  la Fecha De Entrega... ",
                minlength: "Minimo 3 caracter"

            },
            Entrega: {
                required: "Escribi la Entrega... ",
                minlength: "Minimo 3 caracter"
            },
            Nombre: {
                required: "Escribi el Nombre... ",
                minlength: "Minimo 3 caracter"
            },
             Contrasenia: {
                required: "Escribi la Contraseña... ",
                minlength: "Minimo 8 caracter"
            }



        },

        
        submitHandler: function (form) {
            form.submit();
        }
    });

   
}));