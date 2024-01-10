
using System.Diagnostics.Metrics;

Random rand = new Random();

void PrintArray(int[] array)
{
    for (int i = 0; i < 2; i++)
    {
        for (int j = 0; j < 2; j++)
        {
            Console.Write(array[i + j] + " ");
        }

        Console.WriteLine();
    }
}

// All possible inputs for the perceptron
int[] pixels1 = { -1, -1, -1, -1 };
int[] pixels2 = { 1, -1 ,-1, -1 };
int[] pixels3 = { -1, 1 , -1, -1 };
int[] pixels4 = { -1, -1 , 1, -1 };
int[] pixels5 = { -1, -1 , -1, 1 };
int[] pixels6 = { 1, 1,  -1, -1};
int[] pixels7 = { 1, -1 , 1, -1};
int[] pixels8 = { 1, -1 , -1, 1};
int[] pixels9 = { 1, 1 , 1, -1};
int[] pixels10 = {1, 1 , -1, 1};
int[] pixels11 = { 1, 1, 1, 1  };
int[] pixels12 = {-1, -1, 1, 1};
int[] pixels13 = {1, -1, 1, 1};
int[] pixels14 = {-1, 1, 1, 1};
int[] pixels15 = {-1, 1, 1, -1};
int[] pixels16 = {-1, 1 , -1, 1};

// Creating list of all the possible inputs
List<int[]> inputs = new List<int[]>() { pixels1, pixels2, pixels3, pixels4, pixels5, pixels6, pixels7, pixels8, pixels9, pixels10, pixels11, pixels12, pixels13, pixels14, pixels15, pixels16 };


double GetRandomWeight(Random random, double min, double max)
{
    return min + (random.NextDouble() * (max - min));
}



// Setting the learning rate to 0.1
double learningRate = 0.1;

// Special input is always 1
int biasNeuron = 1;

double weight0 = 0;
double weight1 = 0;
double weight2 = 0;
double weight3 = 0;
double weight4 = 0;

// Get random weights for the initial weights
weight0 = GetRandomWeight(rand, -1, 1);
weight1 = GetRandomWeight(rand, -1, 1);
weight2 = GetRandomWeight(rand, -1, 1);
weight3 = GetRandomWeight(rand, -1, 1);
weight4 = GetRandomWeight(rand, -1, 1);


int currentConfig = 0;

int userInput = 1;

// Setting the first epoch to 1
int epoch = 1;

while (userInput != 0)
{

    int input1 = inputs[currentConfig][0];
    int input2 = inputs[currentConfig][1];
    int input3 = inputs[currentConfig][2];
    int input4 = inputs[currentConfig][3];

    Console.WriteLine("Pixels:");
    PrintArray(inputs[currentConfig]);

    int networkOutput = 0;

    double sum = (biasNeuron * weight0) + (input1 * weight1) + (input2 * weight2) + (input3 * weight3) + (input4 * weight4);

    Console.WriteLine("Sum: " + sum);

    int targetOutput = IsLightOrDark(inputs[currentConfig]);
    networkOutput = stepFunction(sum);

    if (networkOutput == 1)
    {
        Console.WriteLine("Network Output: Bright");
    }

    else
    {
        Console.WriteLine("Network Output: Dark");
    }

    // Printing the target output
    if (targetOutput == 1)
    {
        Console.WriteLine("Target Output: Bright");
    }

    else
    {
        Console.WriteLine("Target Output: Dark");
    }

    Console.WriteLine("Weight 0: " + weight0);
    Console.WriteLine("Weight 1: " + weight1);
    Console.WriteLine("Weight 2: " + weight2);
    Console.WriteLine("Weight 3: " + weight3);
    Console.WriteLine("Weight 4: " + weight4);
    Console.WriteLine("Epoch: " + epoch);

    // Recalculating the weights if there is a mismatch
    if (networkOutput != targetOutput)
    {
        Console.WriteLine("Incorrectly categorized, recalculating weights...");
        weightRecalculation(targetOutput, networkOutput);
    }

    Console.WriteLine("Continue? Press 1 for yes or 0 for no:");
    userInput = Int16.Parse(Console.ReadLine());

    if (userInput == 1)
    {
        // Resetting the current config if it is too large and going to next epoch
        if (currentConfig == 15) 
        {
            currentConfig = 0;
            epoch++;
        }

        else
        {
            currentConfig++;
        }
    }

}


// Step function used to align the output to either 1 or -1 depending on the network output
int stepFunction(double sum)
{
    if (sum >= 0)
    {
        return 1;
    }

    else
    {
        return -1;
    }

}


int IsLightOrDark(int[] pixels)
{
    int whiteCount = 0;

    for (int i = 0; i < 4; i++)
    {
        if (pixels[i] == 1)
        {
            whiteCount++;
        }
    }

    // The pixels are bright
    if (whiteCount >= 2)
    {
        return 1;
    }

    // The pixels are dark
    else
    {
        return -1;
    }
}

void weightRecalculation(int targetOutput, int networkOutput)
{
    // Calculating the delta from the bias neuron plus all the inputs
    double delta0 = learningRate * (targetOutput - networkOutput) * 1;
    double delta1 = learningRate * (targetOutput - networkOutput) * inputs[currentConfig][0];
    double delta2 = learningRate * (targetOutput - networkOutput) * inputs[currentConfig][1];
    double delta3 = learningRate * (targetOutput - networkOutput) * inputs[currentConfig][2];
    double delta4 = learningRate * (targetOutput - networkOutput) * inputs[currentConfig][3];

    // Add to the existing weights and receive the new weights for the network
    weight0 += delta0;
    weight1 += delta1;
    weight2 += delta2;
    weight3 += delta3;
    weight4 += delta4;
}
