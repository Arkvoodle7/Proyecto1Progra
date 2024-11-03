package com.pagosmoviles.utils;

import org.bouncycastle.jce.provider.BouncyCastleProvider;
import javax.crypto.Cipher;
import javax.crypto.spec.GCMParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import java.security.Security;
import java.nio.charset.StandardCharsets;
import java.util.Base64;

public class Encrypter {

    private static final String ENCRYPTION_ALGORITHM = "AES/GCM/NoPadding";
    private static final String SECRET_KEY = "YourSecretKey123"; // Debe ser de 16 bytes
    private static final String IV = "RandomInitVector12"; // Debe ser de 12 bytes para GCM

    static {
        Security.addProvider(new BouncyCastleProvider());
    }

    public String encrypt(String plainText) throws Exception {
        Cipher cipher = Cipher.getInstance(ENCRYPTION_ALGORITHM, "BC");
        SecretKeySpec keySpec = new SecretKeySpec(SECRET_KEY.getBytes(StandardCharsets.UTF_8), "AES");
        GCMParameterSpec gcmParameterSpec = new GCMParameterSpec(128, IV.getBytes(StandardCharsets.UTF_8));
        cipher.init(Cipher.ENCRYPT_MODE, keySpec, gcmParameterSpec);
        byte[] encrypted = cipher.doFinal(plainText.getBytes(StandardCharsets.UTF_8));
        return Base64.getEncoder().encodeToString(encrypted);
    }

    public String decrypt(String cipherText) throws Exception {
        Cipher cipher = Cipher.getInstance(ENCRYPTION_ALGORITHM, "BC");
        SecretKeySpec keySpec = new SecretKeySpec(SECRET_KEY.getBytes(StandardCharsets.UTF_8), "AES");
        GCMParameterSpec gcmParameterSpec = new GCMParameterSpec(128, IV.getBytes(StandardCharsets.UTF_8));
        cipher.init(Cipher.DECRYPT_MODE, keySpec, gcmParameterSpec);
        byte[] decodedCipherText = Base64.getDecoder().decode(cipherText);
        byte[] decrypted = cipher.doFinal(decodedCipherText);
        return new String(decrypted, StandardCharsets.UTF_8);
    }
}
