package sample;
//
//import java.sql.*;
import java.text.SimpleDateFormat;
//import java.util.ArrayList;
import java.util.Calendar;

/**
 * Created by Cell on 12/12/2016.
 */
public class TscrumpDAO {


//    public void weatherDAO() {
//        DBmanager dbcon = null;
//        Connection conn = null;
//
//        try {
//            make a connection with the database
//            dbcon = DBmanager.getInstance();
//            conn = dbcon.getConnection();
//
//            Statement stmt = conn.createStatement(); //make a Statement object
//            Calendar cal = Calendar.getInstance();
//            SimpleDateFormat sdft = new SimpleDateFormat("yyyy/MM/dd HH:mm");
//            String time = sdft.format(cal.getTime());
//
//
//date is primary key
//            stmt.executeUpdate("INSERT INTO sensor (Date, Temperature, Pressure, Humidity,Brightness,Precipitation,Latitude,longitude) "
//                    +"VALUES ('2018-05-21 15:00:00',21,1.058,42,58,5,NULL ,NULL )");
//
//            conn.close();
//        } catch (SQLException e) {
//            e.printStackTrace();
//
//        } finally {
//            dbcon.close();
//        }
//    }

    public static void main(String [] args){
        Calendar cal = Calendar.getInstance();
        SimpleDateFormat sdft = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String time = sdft.format(cal.getTime());
        System.out.print(time);
    }
}
