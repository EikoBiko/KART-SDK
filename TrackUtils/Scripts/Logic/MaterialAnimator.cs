using System.Collections;
using UnityEngine;

public class MaterialAnimator : MonoBehaviour
{
    [Tooltip("The material to animate.")]
    public Material targetMaterial; // The material to animate

    [Tooltip("The property name to animate; usually this starts with \"_\". For instance, \"_EmissionMap\". You can find the name of the property if you click Edit on the shader.")]
    public string propertyToAnimate; // The property name to animate

    [Tooltip("The speed of the animation; the number of frames per second. If you set it to zero, you can advance the frames manually with the function FrameStep().")]
    public float speed = 1.0f; // The speed of the animation

    [Tooltip("Array of textures to cycle through.")]
    public Texture[] textures; // Array of textures to cycle through

    // Private variables for internal use
    private int currentTextureIndex = 0;
    private float timer = 0.0f;

    void Update()
    {
        // Update the timer based on the speed
        timer += Time.deltaTime * speed;

        // If the timer exceeds 1.0, move to the next texture
        if (timer >= 1.0f)
        {
            // Reset the timer
            timer = 0.0f;

            // Call the function to update the texture
            FrameStep();
        }
    }

    public void FrameStep()
    {
        // Set the next texture in the array
        currentTextureIndex = (currentTextureIndex + 1) % textures.Length;

        // Apply the texture to the material
        if (targetMaterial != null && textures.Length > 0)
        {
            targetMaterial.SetTexture(propertyToAnimate, textures[currentTextureIndex]);
        }
    }
}
