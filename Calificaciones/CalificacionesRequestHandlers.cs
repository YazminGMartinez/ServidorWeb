public static class CalificacionesRequestHandlers {
    public static IResult MostrarCalificaciones(long NumControl, int parcial){
        if(parcial<1 || parcial>3){
            return Results.BadRequest("El parcial solo puede tener los valores 1, 2 y 3");
        } 
        
        if(NumControl== 22328051051234){
            List<CalificacionMateria>list = new List<CalificacionMateria>();

            CalificacionMateria m1 = new CalificacionMateria();
            m1.Calificaciones = 10;
            m1.Materia = "Programacion Orientada a Objetos";
            m1.Parcial = 1;
            m1.NumControl = 22328051051234;

            CalificacionMateria m2 = new CalificacionMateria();
            m2.Calificaciones = 9;
            m2.Materia = "Programacion Orientada a Eventos";
            m2.Parcial = 1;
            m2.NumControl = 22328051051234;

            CalificacionMateria m3 = new CalificacionMateria();
            m3.Calificaciones = 7.2;
            m3.Materia = "Geometria Analitica";
            m3.Parcial = 1;
            m3.NumControl = 22328051051234;

            CalificacionMateria m4 = new CalificacionMateria();
            m4.Calificaciones = 9;
            m4.Materia = "Etica";
            m4.Parcial = 1;
            m4.NumControl = 22328051051234;

            CalificacionMateria m5 = new CalificacionMateria();
            m5.Calificaciones = 8;
            m5.Materia = "Ingles";
            m5.Parcial = 1;
            m5.NumControl = 22328051051234;

            CalificacionMateria m6 = new CalificacionMateria();
            m6.Calificaciones = 8.5;
            m6.Materia = "Biologia";
            m6.Parcial = 1;
            m6.NumControl = 22328051051234;

            list.Add(m1);
            list.Add(m2);
            list.Add(m3);
            list.Add(m4);
            list.Add(m5);
            list.Add(m6);

            return Results.Ok(list);
    } else {
        return Results.NotFound("No existe un alumno con numero de control "+NumControl);
    }
    }
}