package com.pagosmoviles.utils;

import org.bouncycastle.jce.provider.BouncyCastleProvider;
import org.springframework.stereotype.Component;
import javax.crypto.Cipher;
import javax.crypto.spec.GCMParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import java.security.Security;
import java.nio.charset.StandardCharsets;
import java.util.Base64;
import java.security.SecureRandom;

@Component
public class Encrypter {

    private static final String ENCRYPTION_ALGORITHM = "AES/GCM/NoPadding";
    private static final String SECRET_KEY;

    static {
        Security.addProvider(new BouncyCastleProvider());
        String key = System.getenv("ENCRYPTION_SECRET_KEY");
        if (key == null) {
            throw new IllegalStateException("La variable de entorno ENCRYPTION_SECRET_KEY no está definida.");
        }
        if (key.length() != 16) {
            throw new IllegalArgumentException("La clave secreta debe tener exactamente 16 caracteres.");
        }
        SECRET_KEY = key;
    }

    public String encrypt(String plainText) throws Exception {
        Cipher cipher = Cipher.getInstance(ENCRYPTION_ALGORITHM, "BC");
        SecretKeySpec keySpec = new SecretKeySpec(SECRET_KEY.getBytes(StandardCharsets.UTF_8), "AES");

        byte[] ivBytes = new byte[12];
        SecureRandom secureRandom = new SecureRandom();
        secureRandom.nextBytes(ivBytes);

        GCMParameterSpec gcmParameterSpec = new GCMParameterSpec(128, ivBytes);
        cipher.init(Cipher.ENCRYPT_MODE, keySpec, gcmParameterSpec);
        byte[] encrypted = cipher.doFinal(plainText.getBytes(StandardCharsets.UTF_8));

        byte[] encryptedWithIv = new byte[ivBytes.length + encrypted.length];
        System.arraycopy(ivBytes, 0, encryptedWithIv, 0, ivBytes.length);
        System.arraycopy(encrypted, 0, encryptedWithIv, ivBytes.length, encrypted.length);

        return Base64.getEncoder().encodeToString(encryptedWithIv);
    }

    public String decrypt(String cipherText) throws Exception {
        byte[] encryptedWithIv = Base64.getDecoder().decode(cipherText);

        byte[] ivBytes = new byte[12];
        byte[] encryptedBytes = new byte[encryptedWithIv.length - ivBytes.length];
        System.arraycopy(encryptedWithIv, 0, ivBytes, 0, ivBytes.length);
        System.arraycopy(encryptedWithIv, ivBytes.length, encryptedBytes, 0, encryptedBytes.length);

        Cipher cipher = Cipher.getInstance(ENCRYPTION_ALGORITHM, "BC");
        SecretKeySpec keySpec = new SecretKeySpec(SECRET_KEY.getBytes(StandardCharsets.UTF_8), "AES");
        GCMParameterSpec gcmParameterSpec = new GCMParameterSpec(128, ivBytes);
        cipher.init(Cipher.DECRYPT_MODE, keySpec, gcmParameterSpec);
        byte[] decrypted = cipher.doFinal(encryptedBytes);
        return new String(decrypted, StandardCharsets.UTF_8);
    }
}