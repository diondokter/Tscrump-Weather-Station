package sample;

import java.sql.*;
import java.util.ArrayList;

/**
 * Created by Cell on 12/12/2016.
 */
public class TscrumpDAO {
    private static TscrumpDAO uniqueInstance=null;
    private static Connection conn = null ;
    private TscrumpDAO(DBmanager db){ //precondition dbExisis()
        conn = db.getConnection();
        System.err.println("The database doesn’t exist....");
    }

    public static synchronized TscrumpDAO getInstance(DBmanager db) { // apply singleton design pattern for tscrumpDAO
        if (uniqueInstance == null)
            uniqueInstance = new TscrumpDAO(db);
        return uniqueInstance;
    }

    public ArrayList<String> getCities(String cityName) {
        ArrayList<String> cities = new ArrayList<String>();
        try{
            // Execute the query
            PreparedStatement pstmt = conn.prepareStatement("SELECT city.name FROM city WHERE Name LIKE ?") ;

            pstmt.setString(1, cityName + "%");
            ResultSet rs = pstmt.executeQuery();

            // Loop through the result set
            while( rs.next())
                cities.add(rs.getString("Name") ) ;
        }catch( SQLException se ) { se.printStackTrace(); }
        return cities;
    }
    public ArrayList<String> getCitiesInCountry(String country){
        ArrayList<String> cities = new ArrayList<String>();
        try{
            PreparedStatement pstmt = conn.prepareStatement("SELECT city.name" +
                    " FROM city INNER JOIN country" +
                    " WHERE country.name LIKE ?" +
                    " AND country.code = city.countrycode");
            pstmt.setString(1, country + "%");
            ResultSet rs = pstmt.executeQuery();

            while(rs.next()){
                cities.add(rs.getString("Name"));
            }
        }catch (SQLException e){
            e.printStackTrace();
        }
        return cities;
    }


}
