/*package sample;

import java.sql.*;
import java.util.ArrayList;

/**
 * Created by Cell on 12/12/2016.
 */
/*public class TscrumpDAO {

    public static ArrayList<String> getCities(String cityNamePart) // gives all names of cities starting with cityNamePart. This part you can find in the slides.
    {
        DBmanager dbcon = DBmanager.getInstance();
        Connection conn = dbcon.getConnection();

        ArrayList<String> Names = new ArrayList<>();

        try
        {
            Statement stmt = conn.createStatement();
            stmt.execute("use world");

            PreparedStatement pstmt = conn.prepareStatement("select city.name from city where city.name like \"" + cityNamePart + "%\"");
            ResultSet rs = pstmt.executeQuery();

            while (rs.next())
            {
                Names.add(rs.getString(1));
            }
        }
        catch (SQLException e)
        {
            System.out.println(e.getMessage());
            e.printStackTrace();
        }
        finally
        {
            dbcon.close();
        }

        return Names;
    }

    public static ArrayList<String> getCitiesInCountry(String country) // gives all names of cities in the country. This part you have to made by yourself.
    {
        DBmanager dbcon = DBmanager.getInstance();
        Connection conn = dbcon.getConnection();

        ArrayList<String> Cities = new ArrayList<>();

        try
        {
            Statement stmt = conn.createStatement();
            stmt.execute("use world");

            PreparedStatement pstmt = conn.prepareStatement("select city.name from city join country on country.code = city.countrycode where country.name like \"" + country + "\"");
            ResultSet rs = pstmt.executeQuery();

            while (rs.next())
            {
                Cities.add(rs.getString(1));
            }
        }
        catch (SQLException e)
        {
            System.out.println(e.getMessage());
            e.printStackTrace();
        }
        finally
        {
            dbcon.close();
        }

        return Cities;
    }


}*/
