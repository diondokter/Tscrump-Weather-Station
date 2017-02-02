package sample;

import javax.xml.bind.DatatypeConverter;
import java.nio.ByteBuffer;
import java.sql.*;
import java.text.SimpleDateFormat;
import java.util.Calendar;

import static java.sql.JDBCType.NULL;

/**
 * Created by Ali on 23-10-2016.
 */
public class TestCityDao {
    public static void main(String[] args) {
        DBmanager dbcon = null;
        Connection conn = null;



        try {
            //make a connection with the database
            dbcon = DBmanager.getInstance();
            conn = dbcon.getConnection();

            Statement stmt = conn.createStatement(); //make a Statement object
            Calendar cal = Calendar.getInstance();
            SimpleDateFormat sdft = new SimpleDateFormat("yyyy/MM/dd HH:mm");
            String time = sdft.format(cal.getTime());

//            stmt.executeUpdate("insert into weatherr values (2016-10-21 15:00:00,21,1.058,42,58,5,NULL ,NULL )");
//date is primary key 
            stmt.executeUpdate("INSERT INTO sensor (Date, Temperature, Pressure, Humidity,Brightness,Precipitation,Latitude,longitude) "
                    +"VALUES ('2017-01-2 15:00:00',21,1.058,42,58,5,NULL ,NULL )");
//                    "insert into weatherr values (2016-10-21 15:00:00,21,1.058,42,58,5,NULL ,NULL );");
//            stmt.executeUpdate("insert into Datas (Times,Temperature, Air_Pressure,Humidity,Windspeed,Brightness) values ('" +time +"','"+temperature +"','" +pressure +"','" +humidity +"','" +windspeed +"','"+brightness +"');");

            conn.close();
        } catch (SQLException e) {
            e.printStackTrace();
            System.out.print("lol");
        } finally {
            dbcon.close();
        }
    }
}
//    public int baseToint(String base64){
//        byte []  x = DatatypeConverter.parseBase64Binary(base64);
//        ByteBuffer wrapped = ByteBuffer.wrap(x); // big-endian by default
//        int num = wrapped.getShort();
//        return num;
//    }