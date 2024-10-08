package config;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;

public class ConfigProps {
    private static Properties properties = new Properties();

    static {
        try {
            FileInputStream fileProps = new FileInputStream("local.properties");
            properties.load(fileProps);

        } catch (IOException error) {
            error.printStackTrace();
        }
    }

    public static String getProp(String key) {
        return properties.getProperty(key);
    }
}
