package config;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;

public class ConfigProps {
    private static Properties properties = new Properties();

    static {
        try {
            System.out.println(System.getProperty("user.dir"));
            FileInputStream fileProps = new FileInputStream("bancario/local.properties");
            properties.load(fileProps);

        } catch (IOException error) {
            error.printStackTrace();
        }
    }

    public static String getProp(String key) {
        return properties.getProperty(key);
    }
}
