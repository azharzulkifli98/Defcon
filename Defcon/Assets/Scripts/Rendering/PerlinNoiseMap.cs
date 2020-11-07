using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap
{
    private float xOffset;

    private float yOffset;

    private int octaves;

    private float decayValue;

    private float amplitude;

    private float frequency;

    private float growthValue;

    public PerlinNoiseMap(float xOffset, float yOffset, int octaves, float decayValue, float amplitude, float frequency, float growthValue)
    {
        this.xOffset = xOffset;
        this.yOffset = yOffset;
        this.octaves = octaves;
        this.decayValue = decayValue;
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.growthValue = growthValue;
    }

    public float GetPoint(float x, float y)
    {
        float val = 0;

        float amp = amplitude;

        float freq = frequency;

        for(int i =0; i < octaves; i++)
        {
            val += amp * Mathf.PerlinNoise(xOffset + x * freq, yOffset + y * freq);
            amp *= decayValue;
            freq *= growthValue;
        }

        return val;
    }
}
