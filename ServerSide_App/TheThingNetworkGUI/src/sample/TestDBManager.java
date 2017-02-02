package sample;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

/**
 * Created by Ali on 23-10-2016.
 */
public class TestDBManager {
    public static void main(String[] args){
        DBmanager dbcon = null;
        Connection conn = null;

        try{
            //make a connection with the world database
            dbcon = DBmanager.getInstance();
            conn = dbcon.getConnection();

            Statement stmt = conn.createStatement();
            ResultSet rs = stmt.executeQuery("SELECT country.name FROM country;");

            //make a Statement object
            // execute the sqlstatement the result is resultset
            // print all the countries and use the reusltset
            while (rs.next()) {
                System.out.println(rs.getString(1));
            }
            rs.close();
        } catch (SQLException e){
            e.printStackTrace();
        } finally {
            dbcon.close();
        }
    }
}
