package sample; /**
 * Created by Cell on 10/31/2016.
 */

import java.io.FileInputStream;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Properties;

public class DBmanager {
    private static DBmanager uniqueInstance=null;
    private static Connection con = null ;
    private DBmanager(){
        if(!dbExists()){
            System.err.println("the database doesn’t exist....") ;
        }
}

public static synchronized DBmanager getInstance(){
    if (uniqueInstance == null){
        uniqueInstance = new DBmanager();
    }
    return uniqueInstance;
}

    private boolean dbExists() {
        Boolean exists = false;
        Boolean fileloaded =false;
        try {
            Properties props = new Properties();
            try {
                FileInputStream in = new FileInputStream("path");
                props.load(in);
                in.close();
                fileloaded =true;
            }
            catch (IOException e){
                System.out.println("&quot;IO Exception:&quot; + ioex.getMessage()");
                fileloaded = false;
            }
            if (fileloaded){
                String drivers = props.getProperty("jdbc.drivers");
                if (drivers!=null)System.setProperty("jdbc.drivers",drivers);
                String url = props.getProperty("jdbc.url");
                String user = props.getProperty("jdbc.username");
                String password = props.getProperty("jdbc.password");
                con = DriverManager.getConnection(url,username,password);
                exists = true;
            }
        }
        catch (SQLException sqe){

        }
        return exists;

    }

    public void close(){
        try{
            con.close();
            uniqueInstance = null;
            con=null;

        }
        catch (SQLException e){
            e.printStackTrace();
        }
    }
    public Connection getConnection(){
        return con;
    }
}
