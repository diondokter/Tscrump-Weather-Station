package sample;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.stage.Stage;


public class Controller {
    PahoMqttClient paho;


    @FXML
    Button connectTtn ;

    @FXML
    Tab tabPaneTtn;
    @FXML
    private Button subscribe;

    @FXML
    private Button disconnect;

    @FXML
    private Button reconnect;

    @FXML
    private ToggleButton subDefaultTopic;

    @FXML
    private TextField subTopicFiled;

    @FXML
    private TextField clientIDField;

    @FXML
    private TextField appEUIField;

    @FXML
    private TextField appAKField;

    @FXML
    private ToggleButton pubDefaultTopic;

    @FXML
    private TextField pubTopicField;

    @FXML
    private TextArea pubTextArea;
    @FXML
    private ToggleButton credentialsButton;



    public void connectAction(ActionEvent event)throws Exception{
        paho = new PahoMqttClient(clientIDField.getText(),appEUIField.getText(),appAKField.getText());
        if (paho.getError() == false){
            sceneChanger("TtnClient.fxml",event);
        }
        else {
            System.out.println("SOMTHING WENT WRONG!!!");

        }
//        sceneChanger("TtnClient.fxml",event);


    }

    private void sceneChanger(String fxmlFile,ActionEvent event)throws Exception{
        Parent parent = FXMLLoader.load(getClass().getResource(fxmlFile));
        Scene scene = new Scene(parent);
        Stage stage = (Stage)((Node) event.getSource()).getScene().getWindow();
        stage.setScene(scene);
    }




    public void subscribe(){
        if (subDefaultTopic.isSelected()){
            subTopicFiled.setVisible(false);
            subTopicFiled.setText(paho.receiveNttTopic());
            paho.subscribe(paho.receiveNttTopic());
            paho.weatherDAO();
        }
        else {
            paho.subscribe(subTopicFiled.getText());
            paho.weatherDAO();
        }
    }

    public void publish(){
        if (pubDefaultTopic.isSelected()){
            pubTopicField.setText(paho.sendNttTopic());
            pubTopicField.setDisable(true);
            paho.publish(paho.sendNttTopic(),pubTextArea.getText());
        }
        else {
            paho.publish(pubTopicField.getText(),pubTextArea.getText());
        }
    }

    public void DefaultCredentials(){
        if (credentialsButton.isSelected()){
            clientIDField.setText("Tscrump");
            appEUIField.setText("70B3D57ED00016FA");
            appAKField.setText("PlGBB/sm6KysRHZik1ot9oFBnSPauMyt7MHJXosW+Wc=");
        }
        else {
            clientIDField.clear();
            appEUIField.clear();
            appAKField.clear();

        }
    }

    public void disconnect(){
        paho.disconnect();
    }

    public void reconnect(){
        paho.reconnect();
    }


}
